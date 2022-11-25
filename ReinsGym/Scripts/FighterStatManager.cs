using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using System;


public class FighterStatManager : MonoBehaviour
{
    // Our UI elements

    public TextMeshProUGUI fighterWeight;
    public TextMeshProUGUI fighterStamina;
    public TextMeshProUGUI TrainingStatus;
    public TextMeshProUGUI ExerciseStatus;

    public GameObject TrainingOverlay;


    private void Start()
    {
        // Let's retrieve our fighters current stats right from the start

        GetWeight();
        GetStamina();

        // Ensure our overlay is disabled from the start

        TrainingOverlay.SetActive(false);
    }


    // Remove our training overlay (ran out of time so had to really mickey mouse this) 
    public void RemoveTrainingOverlay()
    {
        TrainingOverlay.SetActive(false);

        // Retrieve updated values while we're at it

        GetWeight();
        GetStamina();

        // Invoke our method to remove the text
        Invoke("RemoveExerciseStatus", 3f);
    }


    // A Mickey mouse function to remove our exercise status text
    public void RemoveExerciseStatus()
    {
        ExerciseStatus.text = "";
    }

    public async void GetWeight()
    {

        // Retrieve current fighter weight

        string chain = GymContractManager.ContractDetails._chain;
        // set network
        string network = GymContractManager.ContractDetails._network;
        // abi in json format
        string abi = GymContractManager.ContractDetails._abi;
        // address of contract
        string contract = GymContractManager.ContractDetails._contract;
        // The method we're calling
        string method = "GetWeight";
        // array of arguments for contract
        string args = "[]";
        // connects to user's browser wallet to call a transaction
        string response = await EVM.Call(chain, network, contract, abi, method, args);
        // display response in game
        fighterWeight.text = response + "KG";
    }


    public async void GetStamina()
    {

        // Retrieve current fighter stamina

        string chain = GymContractManager.ContractDetails._chain;
        // set network
        string network = GymContractManager.ContractDetails._network;
        // abi in json format
        string abi = GymContractManager.ContractDetails._abi;
        // address of contract
        string contract = GymContractManager.ContractDetails._contract;
        // The method we're calling
        string method = "GetStamina";
        // array of arguments for contract
        string args = "[]";
        // connects to user's browser wallet to call a transaction
        string response = await EVM.Call(chain, network, contract, abi, method, args);
        // display response in game
        fighterStamina.text = response + "%";
    }

    public async void DoSpinClass()
    {
        // set chainID, here we use the networkID for goerli
        string chainId = GymContractManager.ContractDetails._chainID;
        // abi in json format
        string abi = GymContractManager.ContractDetails._abi;
        // address of contract
        string contract = GymContractManager.ContractDetails._contract;
        // method you want to write to
        string method = "IncreaseFitness";
        // How much weight are we losing?
        string weight = "10";
        // How much stamina are we gaining?
        string stamina = "5";
        // array of arguments for contract you can also add a nonce here as optional parameter
        string[] obj = { weight, stamina };
        string args = JsonConvert.SerializeObject(obj);
        // GasPrice
        string gasPrice = "";
        // GasLimit
        string gasLimit = "";
        // Our value
        string value = "0";
        // create data for contract interaction
        string data = await EVM.CreateContractData(abi, method, args);
        // send transaction
        try
        {
            // Enable our Training Overlay while we workout
            TrainingOverlay.SetActive(true);
            TrainingStatus.text = "SPINNING VERY FAST!";

            // Send our transaction
            string response = await Web3Wallet.SendTransaction(chainId, contract, value, data, gasLimit, gasPrice);

            // Invoke our remove function (I would have preferred to use a TxCheck here)
            Invoke("RemoveTrainingOverlay", 8f);

            // Set our exercise status text (This would go with a TxCheck as well, but time is against me :o) 
            ExerciseStatus.text = "YOU SMASHED THE SPIN CLASS. YOU FEEL ENERGIZED !";
        }
        catch (Exception e)
        {
            Debug.Log(e);

            TrainingStatus.text = "FAILED TO WORKOUT";
            Invoke("RemoveTrainingOverlay", 3f);
        }
    }



    public async void DoBodyPump()
    {
        // set chainID, here we use the networkID for goerli
        string chainId = GymContractManager.ContractDetails._chainID;
        // abi in json format
        string abi = GymContractManager.ContractDetails._abi;
        // address of contract
        string contract = GymContractManager.ContractDetails._contract;
        // method you want to write to
        string method = "IncreaseFitness";
        // How much weight are we losing?
        string weight = "3";
        // How much stamina are we gaining?
        string stamina = "8";
        // array of arguments for contract you can also add a nonce here as optional parameter
        string[] obj = { weight, stamina };
        string args = JsonConvert.SerializeObject(obj);
        // GasPrice
        string gasPrice = "";
        // GasLimit
        string gasLimit = "";
        // Our value
        string value = "0";
        // create data for contract interaction
        string data = await EVM.CreateContractData(abi, method, args);
        // send transaction
        try
        {
            // Enable our Training Overlay while we workout
            TrainingOverlay.SetActive(true);
            TrainingStatus.text = "PUMPING BIG WEIGHTS!";

            // Send our transaction
            string response = await Web3Wallet.SendTransaction(chainId, contract, value, data, gasLimit, gasPrice);

            // Invoke our remove function (I would have preferred to use a TxCheck here)
            Invoke("RemoveTrainingOverlay", 8f);

            // Set our exercise status text (This would go with a TxCheck as well, but time is against me :o) 
            ExerciseStatus.text = "YOU CHOSE TO NOT SKIP LEG DAY!";
        }
        catch (Exception e)
        {
            Debug.Log(e);

            TrainingStatus.text = "FAILED TO WORKOUT";
            Invoke("RemoveTrainingOverlay", 3f);
        }
    }



    public async void EatCheatMeal()
    {
        // set chainID, here we use the networkID for goerli
        string chainId = GymContractManager.ContractDetails._chainID;
        // abi in json format
        string abi = GymContractManager.ContractDetails._abi;
        // address of contract
        string contract = GymContractManager.ContractDetails._contract;
        // method you want to write to
        string method = "DecreaseFitness";
        // How much weight are we losing?
        string weight = "6";
        // How much stamina are we gaining?
        string stamina = "3";
        // array of arguments for contract you can also add a nonce here as optional parameter
        string[] obj = { weight, stamina };
        string args = JsonConvert.SerializeObject(obj);
        // GasPrice
        string gasPrice = "";
        // GasLimit
        string gasLimit = "";
        // Our value
        string value = "0";
        // create data for contract interaction
        string data = await EVM.CreateContractData(abi, method, args);
        // send transaction
        try
        {
            // Enable our Training Overlay while we workout
            TrainingOverlay.SetActive(true);
            TrainingStatus.text = "EATING BIG PIZZA...";

            // Send our transaction
            string response = await Web3Wallet.SendTransaction(chainId, contract, value, data, gasLimit, gasPrice);

            // Invoke our remove function (I would have preferred to use a TxCheck here)
            Invoke("RemoveTrainingOverlay", 8f);

            // Set our exercise status text (This would go with a TxCheck as well, but time is against me :o) 
            ExerciseStatus.text = "YOU MANAGED TO EAT AN ENTIRE PIZZA IN ONE SITTING";
        }
        catch (Exception e)
        {

            TrainingStatus.text = "YOU FAILED TO EAT";
            Invoke("RemoveTrainingOverlay", 3f);
        }
    }

}
