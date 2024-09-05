using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reactive;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _PotBalance;
    [SerializeField] TextMeshProUGUI _shotText;
    [SerializeField] TextMeshProUGUI _PlayerScore;
    [SerializeField] TextMeshProUGUI _rank1;
    [SerializeField] TextMeshProUGUI _rank2;
    [SerializeField] TextMeshProUGUI _rank3;
    [SerializeField] TextMeshProUGUI _rank1Address;
    [SerializeField] TextMeshProUGUI _rank2Address;
    [SerializeField] TextMeshProUGUI _rank3Address;
    private void Start()
    {
        // this function get both shots and score
        GetComponent<CallPubQuestSmartContract>().getShots();

        GetComponent<CallPubQuestSmartContract>().getHighScores();
        GetComponent<CallPubQuestSmartContract>().getPotBalance();

    }
    public void setShotText(string text)
    {
        _shotText.text = "You have " + text + " remaining shot";
    }
    public void setScoreText(string text)
    {
        _PlayerScore.text = "Your Best Score is " + text;
    }


    public void setPotText(BigInteger pot)
    {
        _PotBalance.text = PotFormatter(pot);
    }

    public void setHighScore(BigInteger rank1, BigInteger rank2, BigInteger rank3, string add1, string add2, string add3)
    {
        _rank1.text = PotFormatter(rank1);
        _rank2.text = PotFormatter(rank2);
        _rank3.text = PotFormatter(rank3);
        _rank1Address.text = AddressFormatter(add1);
        _rank2Address.text = AddressFormatter(add2);
        _rank3Address.text = AddressFormatter(add3);
    }

    // nice coding ...
    public void setHighfirstScore(BigInteger rank)
    {
        _rank1.text = PotFormatter(rank);
    }
    public void setHighsecondScore(BigInteger rank)
    {
        _rank2.text = PotFormatter(rank);
    }

    public void setHighthirsScore(BigInteger rank)
    {
        _rank3.text = PotFormatter(rank);
    }
    public void setHighthirsaddress(string add)
    {
        _rank3Address.text = AddressFormatter(add);
    }
    public void setHighsecondaddress(string add)
    {
        _rank2Address.text = AddressFormatter(add);
    }
    public void setHighfirstaddress(string add)
    {
        _rank1Address.text = AddressFormatter(add);
    }



    public string AddressFormatter(string address)
    {
        if (address.Length < 40)
        {
            return "0x00";
        }
        return address.Substring(0, 20);
    }
    public string PotFormatter(BigInteger pot)
    {
        var ss = pot.ToString();
        if (ss.Length < 5)
        {
            return ss;
        }

        var first5digit = ss.Substring(0, 5);

        var leftdigit = ss.Substring(5, ss.Length - 5).Length.ToString();

        return first5digit + " (*10^" + leftdigit + " )";

    }
}
