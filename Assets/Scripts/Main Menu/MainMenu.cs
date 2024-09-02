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





public class MainMenu : MonoBehaviour
{





    [SerializeField] private TextMeshProUGUI _connectDisconnectButton;
    [SerializeField] private TextMeshProUGUI _textMeshProAddressss;
    [SerializeField] private SceneController _sceneController;
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

        Debug.LogError("Message received");
        UnityThread.executeInUpdate(() => { onTransactionResult?.Invoke(sender, e); });
    }


    void OnWalletAuthorized(object sender, EventArgs e)
    {
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

        Debug.LogError("Wallet is Disconnecteddd");
        onWalletDisconnected?.Invoke(this, EventArgs.Empty);
        _textMeshProAddressss.text = "Wallet Disconnected";
    }

    /// <summary>Raised when the wallet is ready.</summary>
    private void walletReady(object sender, EventArgs e)
    {

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


    private void Awake()
    {
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
        Debug.Log("Game Scene Loading");
        _sceneController.LoadScene("Game");
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

    }
    public void Connect()
    {
        if (MetaMaskUnity.Instance.Wallet.IsConnected)
        {
            MetaMaskUnity.Instance.Wallet.Disconnect();

        }
        else
        {
            Debug.LogError("Connecting");
            MetaMaskUnity.Instance.ConnectAndSign("Hey thrrrrrrr");

        }


    }
    public void Exit()
    {

        // Application.Quit();
    }







}
