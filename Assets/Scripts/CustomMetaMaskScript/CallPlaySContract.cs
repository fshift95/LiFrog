using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MetaMask.Unity;
using evm.net;
using evm.net.Models;
using Playspace;
using System;
using MetaMask.Unity.Samples;
using UnityEngine.SceneManagement;
public class CallPlaySContract : MonoBehaviour
{

    private string PlaySmartContractAddress = "0xf8A80B46fA1CD68e061E60B5F71BDde32B7F3440";
    //private Playspace.Playspace usdc;
    private void Start()
    {


    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");

    }
    public void PlayGameCroak()
    {
        SceneManager.LoadScene("GameCroak");

    }
    public async void SCAddPlayer()
    {
        CallOptions options = new CallOptions();
        options.Value = "2000";
        ModalData modalData = new ModalData();
        modalData.headerText = "Sign Sent";
        modalData.bodyText = "Sign has been sent to your wallet, please ensure you have the application open on your device";
        UIModalManager.Instance.OpenModal(modalData);

        var name = GetComponent<TextController>().getInputName();
        if (name.Length > 0)
        {
            var metaMask = MetaMaskUnity.Instance.Wallet;
            Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);
            var balance = await usdc.Addplayer(MetaMaskUnity.Instance.Wallet.ConnectedAddress, name);

            Invoke("getPlayer", 3);
            GetComponent<TextController>().ShowNameEdit();




        }

    }
    public void OpenModal()
    {
        ModalData modalData = new ModalData();
        modalData.headerText = "test Modal";
        modalData.bodyText = "Sign has been sent to your wallet, please ensure you have the application open on your device";
        UIModalManager.Instance.OpenModal(modalData);

    }

    public async void getPlayer()
    {


        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        Debug.LogWarning("Address issssssssssss" + MetaMaskUnity.Instance.Wallet.ConnectedAddress);

        var playerName = await usdc.GetPlayer(MetaMaskUnity.Instance.Wallet.ConnectedAddress);

        if (playerName.Length == 0)
        {
            GetComponent<TextController>().ShowNameInput();
        }
        else
        {
            GetComponent<TextController>().ShowNameEdit();
            GetComponent<TextController>().setNameText("Name: " + playerName.ToString());
        }




    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public async void getScore()
    {

        var metaMask = MetaMaskUnity.Instance.Wallet;


        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        var score = await usdc.GetScore(MetaMaskUnity.Instance.Wallet.ConnectedAddress);
        GetComponent<PreLoadGameSolo>().setScoreText(score.ToString());

    }
    public async void setScore(int score)
    {
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        var txt = await usdc.SetScore(MetaMaskUnity.Instance.Wallet.ConnectedAddress, score);

    }

    private void showText(string text)
    {
        GetComponent<TextController>().setCornerText(text);
    }
    private int InputTextScore()
    {
        Debug.LogError(GetComponent<TextController>().getInputTextScore());
        return GetComponent<TextController>().getInputTextScore();
    }


    public void CallDataPlatScmartContract()
    {
        Debug.LogError("loading Smart Contractddsfsdfsdfdf");
        getPlayer();
        getScore();

    }


}
