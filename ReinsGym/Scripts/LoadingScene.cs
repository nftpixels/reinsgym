using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LoadingScene : MonoBehaviour
{

    // Our UI Elements (Just using arrays to make things more exciting)
    public GameObject[] LoadingItems;
    public GameObject[] ConnectItems;

    public TextMeshProUGUI TitleText;



    private void Start()
    {
        Invoke("FinishedLoading", 3f);
    }


    // Remove our loading bar and start the wallet connect process
    public void FinishedLoading()
    {
        // Remove our loading items

        foreach (GameObject _Loading in LoadingItems)
        {
            _Loading.SetActive(false);
        }

        // Enable our connection items

        foreach (GameObject _connecting in ConnectItems)
        {
            _connecting.SetActive(true);
        }

        // Change our title text

        TitleText.text = "REINS GYM";

    }


    // Our Login Function for Web3Wallet

    async public void OnLogin()
    {
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = expirationTime.ToString();
        // sign message
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now)
        {
            // save account
            PlayerPrefs.SetString("Account", account);
            // load next scene
            SceneManager.LoadScene("CheckMembership");
        }
    }

}
