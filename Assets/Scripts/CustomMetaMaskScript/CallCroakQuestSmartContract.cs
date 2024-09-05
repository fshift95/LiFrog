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
using JetBrains.Annotations;



public class CallCroakSmartContract : MonoBehaviour
{
    private string _smartContractAddress = "0xd7d19E41bC53d12AaC4C4f3B0A95E1E269F84E68";

    public delegate void AsyncCallback(IAsyncResult payFee);

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public async void saveScore(int score)
    {
        isCWonnected();
        SignSentAlert();
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Croak.Croak pubQuestContract = Contract.Attach<Croak.Croak>(metaMask, _smartContractAddress);
        var shots = await pubQuestContract.SetScore(score);
    }

    public async void getScores()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Croak.Croak pubQuestContract = Contract.Attach<Croak.Croak>(metaMask, _smartContractAddress);
        var score = await pubQuestContract.GetPlayerScore(metaMask.SelectedAddress);
        GetComponent<PreLoadGameCroak>().setScoreText(score.ToString());

    }
    public async void getHighScores()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Croak.Croak pubQuestContract = Contract.Attach<Croak.Croak>(metaMask, _smartContractAddress);
        var score1 = await pubQuestContract.GetHighFirstScore();
        GetComponent<PreLoadGameCroak>().setHighfirstScore(score1);
        var score2 = await pubQuestContract.GetHighsecondScore();
        GetComponent<PreLoadGameCroak>().setHighsecondScore(score2);
        var score3 = await pubQuestContract.GetHighthirdScore();
        GetComponent<PreLoadGameCroak>().setHighthirsScore(score3);

        // var add1 = await pubQuestContract.GetHighfirstdadd();
        // GetComponent<PreLoadGameCroak>().setHighfirstaddress(add1.ToString());
        // var add2 = await pubQuestContract.GetHighseconddadd();
        // GetComponent<PreLoadGameCroak>().setHighsecondaddress(add2.ToString());
        // var add3 = await pubQuestContract.GetHighthirdadd();
        // GetComponent<PreLoadGameCroak>().setHighthirsaddress(add3.ToString());




        //Tuple<BigInteger, BigInteger, BigInteger, EvmAddress, EvmAddress, EvmAddress> getvalue = scores;
        //   GetComponent<PreLoadGameCroak>().setHighScore(scores.Item1, scores.Item2, scores.Item3, scores.Item4, scores.Item5, scores.Item6);

        // Debug.LogError("scores item" + scores.Item1 + scores.Item2 + scores.Item4);

    }
    public async void getTotalStokenBalance()
    {
        if (!isCWonnected())
        {
            return;
        }
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Croak.Croak pubQuestContract = Contract.Attach<Croak.Croak>(metaMask, _smartContractAddress);
        var score = await pubQuestContract.GetTotalStokenBalance();
        GetComponent<PreLoadGameCroak>().setPotText(score);

    }

    public void playCroak()
    {
        if (!isCWonnected())
        {
            return;
        }
        SceneManager.LoadScene("GameCroak");
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


}
