using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymContractManager : MonoBehaviour
{

    // Quick static class to keep it clean when referencing our contract details. 
    public class ContractDetails
    {

        // Our preferred chainID
        public static string _chainID = "338";

        // Our preferred chain
        public static string _chain = "cronos";

        // Our preferred network
        public static string _network = "testnet";

        // Our Contract Address
        public static string _contract = "0x54555401A1Ea4e9ce6b3F929b014761869795a81";

        // Our Contract ABI
        public static string _abi = "[ { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_weight\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_stamina\", \"type\": \"uint256\" } ], \"name\": \"DecreaseFitness\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"GetStamina\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"GetWeight\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [ { \"internalType\": \"uint256\", \"name\": \"_weight\", \"type\": \"uint256\" }, { \"internalType\": \"uint256\", \"name\": \"_stamina\", \"type\": \"uint256\" } ], \"name\": \"IncreaseFitness\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"ResetStats\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"SignUp\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"stamina\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"weight\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
    }


}
