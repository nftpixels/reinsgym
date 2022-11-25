using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;

public class MembershipManagement : MonoBehaviour
{
    // Public Variables

    public string UserIP;
    public string UserWallet;

    // Authentication Boolean
    public bool isRegistered;

    // Our status text
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI RegistrationStatusText;

    // Our registration check panel (Just to make it look like we're loading something)
    public GameObject RegistrationLoadingScreen;



    // Start is called before the first frame update
    void Start()
    {
        // Fetch the player public IP address from IPIFY
        StartCoroutine(GetPublicIP("https://corsproxy.io/?https://api.ipify.org"));

        // Fetch our Player Data
        StartCoroutine(GetPlayerData("https://corsproxy.io/?https://revathy.pawsndisguise.co.nz/DatabaseExample/getPlayerData.php"));

        // Set our player wallet
        UserWallet = PlayerPrefs.GetString("Account");

        // Clear our text
        StatusText.text = "";

        // Disable our loading screen at start
        RegistrationLoadingScreen.SetActive(false);
    }



    // This function will get the connecting players IP address.
    public IEnumerator GetPublicIP(string API)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success: // If we retrieve the data successfully...

                    // Set our PublicIP
                    UserIP = webRequest.downloadHandler.text;

                    break;
            }
        }
    }



    // Function to check if we are registered
    public void EnterGym()
    {
        if (!isRegistered)
        {
            // Tell the user that they're not yet registered
            StatusText.text = "YOU DON'T HAVE ANY ACTIVE MEMBERSHIPS. PLEASE SIGN-UP!";
        }
        else
        {
            // Allow the user to enter the gym
            SceneManager.LoadScene("ReinsGym");
        }
    }




    // We use this function to download our MySQL Database entries and check the connecting player against it.
    public IEnumerator GetPlayerData(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success: // If we retrieve the data successfully...

                    string rawresponse = webRequest.downloadHandler.text;

                    string[] users = rawresponse.Split("*"); // We just santize the data a bit

                    for (int i = 0; i < users.Length; i++) // And then iterate through the results
                    {
                        string[] userinfo = users[i].Split(',');

                        foreach (string data in userinfo)
                        {
                            if (!userinfo[0].Contains(UserWallet) && (!userinfo[1].Contains(UserIP))) // If our connected wallet matches an entry in our database...
                            {
                                isRegistered = false; // They are not a registered player
                            }
                            else if (userinfo[0].Contains(UserWallet) && (userinfo[1].Contains(UserIP)))
                            {
                                isRegistered = true; // They are a registered player
                            }
                        }
                    }
                    break;
            }
        }
    }


    public void RegisterNewPlayer()
    {
        // Enable our loading screen
        RegistrationLoadingScreen.SetActive(true);

        StartCoroutine(AddData());
    }


    // We use this function to POST, or add data to our existing database
    public IEnumerator AddData()
    {

        WWWForm form = new WWWForm();
        form.AddField("UserWallet", UserWallet); // Our Player Wallet 
        form.AddField("UserIP", UserIP); // Our Player IP Address


        UnityWebRequest www = UnityWebRequest.Post("https://corsproxy.io/?https://revathy.pawsndisguise.co.nz/DatabaseExample/AddPlayerData.php", form); // Our Script URL
        {
            yield return www.SendWebRequest();

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:

                    Debug.LogError("Error: " + www.error);
                    break;

                case UnityWebRequest.Result.DataProcessingError:

                    Debug.LogError("Error: " + www.error);
                    break;

                case UnityWebRequest.Result.Success:

                    Debug.Log("Data uploaded successfully");
                    Debug.Log("Wallet: " + UserWallet);
                    Debug.Log("IP Address: " + UserIP);

                    // Remove our loading screen once the data has been uploaded successfully
                    Invoke("RemoveLoadingScreen", 3f);

                    // Fetch our updated player data
                    StartCoroutine(GetPlayerData("https://corsproxy.io/?https://revathy.pawsndisguise.co.nz/DatabaseExample/getPlayerData.php"));

                    break;
            }
        }
    }


    // A Mickey Mouse function to remove our loading screen (Assuming it's a sucess - Aaaaah I have like 15 minutes left and I'm doing SQL stuff. Why did I think this was a good idea haha!?!?!
    public void RemoveLoadingScreen()
    {
        RegistrationLoadingScreen.SetActive(false);

        // Set our status text
        StatusText.text = "YOU NOW HAVE A PREMIUM MEMBERSHIP. PLEASE ENTER THE GYM!";

        // Fetch our Player Data
        StartCoroutine(GetPlayerData("https://corsproxy.io/?https://revathy.pawsndisguise.co.nz/DatabaseExample/getPlayerData.php"));
    }


    // Call our solidity function to assign stats to the new user
    public async void SignUp()
    {

        if (!isRegistered)
        {
            string chain = GymContractManager.ContractDetails._chain;
            // set network
            string network = GymContractManager.ContractDetails._network;
            // abi in json format
            string abi = GymContractManager.ContractDetails._abi;
            // address of contract
            string contract = GymContractManager.ContractDetails._contract;
            // The method we're calling
            string method = "SignUp";
            // array of arguments for contract
            string args = "[]";
            // connects to user's browser wallet to call a transaction

            try
            {
                // Enable our Training Overlay while we workout
                RegistrationLoadingScreen.SetActive(true);

                // Send our transaction
                string response = await EVM.Call(chain, network, contract, abi, method, args);

                // Invoke our remove function (I would have preferred to use a TxCheck here)
                Invoke("RemoveLoadingScreen", 8f);

                // "Register" our player data to the database.
                StartCoroutine(AddData());


            }
            catch (Exception e)
            {
                Debug.Log(e);

                RegistrationStatusText.text = "FAILED TO REGISTER";
                Invoke("RemoveLoadingScreen", 3f);
            }
        }
        else
        {
            // Set our status text
            StatusText.text = "YOU CANNOT REGISTER AGAIN. PLEASE ENTER THE GYM!";
        }
    }



}


