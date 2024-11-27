using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaMask.Unity;
using evm.net;
using evm.net.Models;
using MetaMask.Unity.Samples;
using UnityEngine.SceneManagement;
using MetaMask;
using MetaMask.Models;
using System;

using System.Numerics;
using Unity.VisualScripting;



public class CallPubQuestSmartContract : MonoBehaviour
{
    // private string _smartContractAddress = "0x4AC32bF046F4B09147078014fcabf5EEE09e59d7"; prev Address
    private string _smartContractAddress = "0x3984AAb5B0404214646c52fcFBD21A2e0823B68b";
    public async void getPotBalance()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);
        var score = await pubQuestContract.GetPubBalance();
        GetComponent<PreLoadGame>().setPotText(score);

    }




    public async void burnShotAndPlay()
    {
        if (!isCWonnected())
        {
            return;
        }
        ModalData modalData = new ModalData();
        modalData.headerText = "Sign Sent";
        modalData.bodyText = "Please sign to enter the Frogly Game Quest ";
        UIModalManager.Instance.OpenModal(modalData);


        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);
        var shots = await pubQuestContract.GetPlayerShots(metaMask.SelectedAddress);


        if (shots > 0)
        {
            // var payFee = await pubQuestContract.BurnShot(metaMask.ConnectedAddress);
            SceneManager.LoadScene("GameQuest");


        }
        else
        {
            ModalData modalData2 = new ModalData();
            modalData2.headerText = "Not Enought Balance";
            modalData2.bodyText = "Please Pay entrance fee first";
            UIModalManager.Instance.OpenModal(modalData2);
        }



    }
    public delegate void AsyncCallback(IAsyncResult payFee);

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public async void saveScore(int score) //burn shot and save score
    {
        SignSentAlert();

        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);


        //await pubQuestContract.SetScore(score);

        //the name of method on smartcontract should be more semantic
        await pubQuestContract.SetScores(score, metaMask.ConnectedAddress);

    }

    public async void getHighScores()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);
        var score1 = await pubQuestContract.GetHighFirstScore();
        GetComponent<PreLoadGame>().setHighfirstScore(score1);
        var score2 = await pubQuestContract.GetHighsecondScore();
        GetComponent<PreLoadGame>().setHighsecondScore(score2);
        var score3 = await pubQuestContract.GetHighthirdScore();
        GetComponent<PreLoadGame>().setHighthirsScore(score3);

    }


    private void Start()
    {

    }
    public async void getShots()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);
        var shots = await pubQuestContract.GetPlayerShots(metaMask.SelectedAddress);
        var score = await pubQuestContract.GetPlayerScore(metaMask.SelectedAddress);

        GetComponent<PreLoadGame>().setShotText(shots.ToString());
        GetComponent<PreLoadGame>().setScoreText(score.ToString());

    }

    private bool isCWonnected()
    {
        if (!MetaMaskUnity.Instance.Wallet.IsConnected)
        {
            ModalData modalDataC = new ModalData();
            modalDataC.headerText = "Connect Your Wallet";
            modalDataC.bodyText = "Go Back To Main Menu and Connect your wallet";
            UIModalManager.Instance.OpenModal(modalDataC);
            return false;
        }
        else
        {
            return true;
        }
    }
    private void SignSentAlert()
    {

        ModalData modalData = new ModalData();
        modalData.headerText = "Sign Sent";
        modalData.bodyText = "Sign has been sent to your wallet, please ensure you have the application open on your device";
        UIModalManager.Instance.OpenModal(modalData);
    }

    public async void buyShots()
    {
        if (!isCWonnected())
        {
            return;
        }
        SignSentAlert();



        CallOptions options = new CallOptions();
        options.Value = "B5E620F48000";
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Froggy.Froggy pubQuestContract = Contract.Attach<Froggy.Froggy>(metaMask, _smartContractAddress);
        var shots = await pubQuestContract.PayEntranceFee(metaMask.ConnectedAddress, options);

        getShots();

        // var wallet = MetaMaskUnity.Instance.Wallet;
        // var transactionParams = new MetaMaskTransaction
        // {
        //     To = "0x77c6163727958CBa3A9BDa61BE33925F811bF13B",
        //     From = MetaMaskUnity.Instance.Wallet.SelectedAddress,
        //     Value = "B5E620F48000" /// its fucked up and show a totally different and big number
        // };

        // var request = new MetaMaskEthereumRequest
        // {
        //     Method = "eth_sendTransaction",
        //     Parameters = new MetaMaskTransaction[] { transactionParams }
        // };
        // await wallet.Request(request);
    }
}
