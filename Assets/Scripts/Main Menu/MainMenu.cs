using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MetaMask.Unity;
using System.Threading.Tasks;

using System;

using System.Text;
using System.Text.Json.Serialization;

using MetaMask.Cryptography;
using MetaMask.Models;
using MetaMask.SocketIOClient;
using Newtonsoft.Json;
using MetaMask;
using Org.BouncyCastle.Math;

using TMPro;

using MetaMask.Contracts;
using MetaMask.Unity.Contracts;
using MetaMask.Transports.Unity.UI;
using MetaMask.Unity.Samples;
using evm.net.Models;






public class MainMenu : MonoBehaviour
{





    [SerializeField] private TextMeshProUGUI _connectDisconnectButton;
    [SerializeField] private TextMeshProUGUI _textMeshProAddressss;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _buttonQuest;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _disConnectButoon;
    #region Events

    /// <summary>Raised when the wallet is connected.</summary>
    public event EventHandler onWalletConnected;
    /// <summary>Raised when the wallet is disconnected.</summary>
    public event EventHandler onWalletDisconnected;
    /// <summary>Raised when the wallet is ready.</summary>
    public event EventHandler onWalletReady;
    /// <summary>Raised when the wallet is paused.</summary>
    public event EventHandler onWalletPaused;
    /// <summary>Raised when the user signs and sends the document.</summary>
    public event EventHandler onSignSend;
    /// <summary>Occurs when a transaction is sent.</summary>
    public event EventHandler onTransactionSent;
    /// <summary>Raised when the transaction result is received.</summary>
    /// <param name="e">The event arguments.</param>
    private bool _loadDataFromContract = true;
    public event EventHandler<MetaMaskEthereumRequestResultEventArgs> onTransactionResult;

    /// <summary>Raised when the transaction result is received.</summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    public void TransactionResult(object sender, MetaMaskEthereumRequestResultEventArgs e)
    {
        string address = MetaMaskUnity.Instance.Wallet?.ConnectedAddress;
        if (address != null)
        {
            _textMeshProAddressss.text = address;
        }
        UnityThread.executeInUpdate(() => { onTransactionResult?.Invoke(sender, e); });




        if (_loadDataFromContract)
        {
            if (MetaMaskUnity.Instance.Wallet.ConnectedAddress.Length > 20)
            {
                gameObject.GetComponent<CallPlaySContract>().CallDataPlatScmartContract();
                _loadDataFromContract = false;
            }

        }


    }

    private void OnSignSend(object sender, EventArgs e)
    {

        ModalData modalData = new ModalData();
        modalData.headerText = "Sign Sent";

        modalData.bodyText = "Sign has been sent to your wallet, please ensure you have the application open on your device";
        UIModalManager.Instance.OpenModal(modalData);
    }

    private void OnTransactionSent(object sender, EventArgs e)
    {
        ModalData modalData = new ModalData();
        modalData.headerText = "Transaction Sent";
        modalData.bodyText = "Transaction Sent has been sent to your wallet, please ensure you have the application open on your device";
        UIModalManager.Instance.OpenModal(modalData);
    }

    private void OnTransactionResult(object sender, MetaMaskEthereumRequestResultEventArgs e)
    {


        ModalData modalData = new ModalData();
        modalData.type = ModalData.ModalType.Transaction;
        modalData.headerText = "Result Received";
        modalData.bodyText = string.Format("<b>Method Name:</b><br> {0} <br> <br> <b>Transaction Details:</b><br>{1}", e.Request.Method, e.Result.ToString());
        UIModalManager.Instance.OpenModal(modalData);




    }

    void OnWalletAuthorized(object sender, EventArgs e)
    {
    }
    public void VersionCheck()
    {
        CallOptions options = new CallOptions();
        options.Value = "550000000000000";
        Debug.LogError("SDK " + options.Value);
    }

    private void OnDisable()
    {
        MetaMaskUnity.Instance.Events.WalletAuthorized -= walletConnected;
        MetaMaskUnity.Instance.Events.WalletDisconnected -= walletDisconnected;
        MetaMaskUnity.Instance.Events.WalletReady -= walletReady;
        MetaMaskUnity.Instance.Events.WalletPaused -= walletPaused;

    }

    /// <summary>Raised when the wallet is disconnected.</summary>
    private void walletDisconnected(object sender, EventArgs e)
    {
        //  _connectDisconnectButton.text = "Connect";
        //   _connectDisconnectButton.color = Color.green;

        //  _buttonPlay.interactable = false;
        //  _buttonQuest.interactable = false;
        _connectButton.gameObject.SetActive(true);
        _disConnectButoon.gameObject.SetActive(false);
        Debug.LogError("Wallet is Disconnecteddd");
        onWalletDisconnected?.Invoke(this, EventArgs.Empty);
        _textMeshProAddressss.text = "Wallet Disconnected";
    }

    /// <summary>Raised when the wallet is ready.</summary>
    private void walletReady(object sender, EventArgs e)
    {

        //  _buttonPlay.interactable = true;
        //  _buttonQuest.interactable = true;
        _connectButton.gameObject.SetActive(false);
        _disConnectButoon.gameObject.SetActive(true);



        // _connectDisconnectButton.text = "Disconnect";
        // _connectDisconnectButton.color = Color.red;

        // UnityThread.executeInUpdate(() =>
        // {


        //     Debug.LogError("Wallet is length is  sss" + MetaMaskUnity.Instance.Wallet.ConnectedAddress.Length);

        //     _textMeshProAddressss.text = address;
        //     onWalletReady?.Invoke(this, EventArgs.Empty);
        // });

    }

    /// <summary>Raised when the wallet is paused.</summary>
    private void walletPaused(object sender, EventArgs e)
    {
        UnityThread.executeInUpdate(() => { onWalletPaused?.Invoke(this, EventArgs.Empty); });
    }

    #endregion


    /// <summary>Raised when the wallet is connected.</summary>
    private void walletConnected(object sender, EventArgs e)
    {
        UnityThread.executeInUpdate(() =>
        {
            onWalletConnected?.Invoke(this, EventArgs.Empty);

        });
    }


    private bool _firstInitializing = false;


    private void Start()
    {
        Time.timeScale = 1;
        Debug.Log("atart..........");

        if (MetaMaskUnity.Instance.Wallet != null)
        {

            // _buttonPlay.interactable = true;
            // _buttonQuest.interactable = true;

        }
        else
        {
            Debug.Log("Wallete is not Connected Yet");
        }

        if (!_firstInitializing)

        {

            MetaMaskUnity.Instance.Initialize();

            MetaMaskUnity.Instance.Events.WalletAuthorized += walletConnected;
            MetaMaskUnity.Instance.Events.WalletDisconnected += walletDisconnected;
            MetaMaskUnity.Instance.Events.WalletReady += walletReady;
            MetaMaskUnity.Instance.Events.WalletPaused += walletPaused;
            MetaMaskUnity.Instance.Events.EthereumRequestResultReceived += TransactionResult;

            _firstInitializing = true;
        }


    }


    public void Play()
    {

        _sceneController.LoadScene("LoadGame");
    }
    public void PlayCroak()
    {

        _sceneController.LoadScene("LoadCroakMode");
    }
    public void CroakMode()
    {

        _sceneController.LoadScene("LoadCroakMode");
    }
    public void PlayQuest()
    {

        _sceneController.LoadScene("LoadQuest");
    }
    public void Check()
    {
        if (MetaMaskUnity.Instance.Wallet.IsConnected)
        {
            Debug.Log("is connected - adress is " + MetaMaskUnity.Instance.Wallet.ConnectedAddress);
            _textMeshProAddressss.text = MetaMaskUnity.Instance.Wallet.ConnectedAddress;
        }
        else
        {
            Debug.Log("is NNNot connected");
        }
        if (MetaMaskUnity.Instance.Wallet.SelectedAddress != null)
        {
            Debug.Log("is connected - adress is " + MetaMaskUnity.Instance.Wallet.SelectedAddress);
            _textMeshProAddressss.text = MetaMaskUnity.Instance.Wallet.SelectedAddress;
        }

    }
    public void Connect()
    {
        //crefactore this
        if (MetaMaskUnity.Instance.Wallet.IsConnected)
        {
            // MetaMaskUnity.Instance.Wallet.Disconnect();
        }
        else
        {
            MetaMaskUnity.Instance.ConnectAndSign("Welcome to Frogly Game");
            MetaMaskUnity.Instance.SaveSession();
        }
    }
    public void Disconnect()
    {
        MetaMaskUnity.Instance.Wallet.Disconnect();
    }
    public void Exit()
    {

        // Application.Quit();
    }







}
