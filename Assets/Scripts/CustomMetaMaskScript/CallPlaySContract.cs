using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MetaMask.Unity;
using evm.net;
using evm.net.Models;
using Playspace;
using System;
public class CallPlaySContract : MonoBehaviour
{

    private string PlaySmartContractAddress = "0x8216B83190F973D52E44CadD3088560F9E301100";
    //private Playspace.Playspace usdc;
    private void Start()
    {


    }
    public async void SCAddPlayer()
    {

        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);
        var balance = await usdc.Addplayer(MetaMaskUnity.Instance.Wallet.SelectedAddress, "ddfdfdf");
        Debug.Log(balance);
    }

    public async void getPlayer()
    {
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        var playerName = await usdc.GetPlayer(MetaMaskUnity.Instance.Wallet.SelectedAddress);
        showText("player name is : " + playerName.ToString());
        Debug.Log(playerName);
    }
    public async void getScore()
    {
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        var score = await usdc.GetScore(MetaMaskUnity.Instance.Wallet.SelectedAddress);
        showText("yor Score is : " + score.ToString());
        Debug.Log(score);
    }
    public async void setScore()
    {
        var metaMask = MetaMaskUnity.Instance.Wallet;
        Playspace.Playspace usdc = Contract.Attach<Playspace.Playspace>(metaMask, PlaySmartContractAddress);

        var txt = await usdc.SetScore(MetaMaskUnity.Instance.Wallet.SelectedAddress, InputTextScore());
        showText("seting score to 25000 " + txt.ToString());
        Debug.Log(txt);
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


}
