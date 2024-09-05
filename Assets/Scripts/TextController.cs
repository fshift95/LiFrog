using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{



    [SerializeField] TextMeshProUGUI _textMeshProCornerText;
    [SerializeField] TextMeshProUGUI _textMeshProName;
    [SerializeField] TextMeshProUGUI _textMeshProScore;
    [SerializeField] TextMeshProUGUI _textMeshProQuestBalance;
    [SerializeField] TextMeshProUGUI _textMeshProScoreList;
    [SerializeField] TextMeshProUGUI _textMeshProtextInput;
    [SerializeField] GameObject _nameInputBundle;
    [SerializeField] GameObject _nameInputEdit;

    public void setCornerText(string text)
    {
        _textMeshProCornerText.text = text;
    }
    public void setNameText(string text)
    {
        _textMeshProName.text = text;
    }
    public void setScoreText(string text)
    {
//        _textMeshProScore.text = text;
    }
    public void setQuestText(string text)
    {
       // _textMeshProQuestBalance.text = text;
    }
    public void setScoreList(string text)
    {
     //   _textMeshProScoreList.text = text;
    }
    public void ShowNameInput()
    {
        // _nameInputBundle.SetActive(true);
        // _nameInputEdit.SetActive(false);
    }
    public void ShowNameEdit()
    {

        _nameInputEdit.SetActive(true);
        _nameInputBundle.SetActive(false);
        //choose better names
    }
    public int getInputTextScore()
    {

        int result = 2;


        var len = _textMeshProtextInput.text.Length;
        Debug.LogError("textttttt iss   " + _textMeshProtextInput.text.Substring(0, len - 1));
        Debug.LogError("sdfsdfsdf" + int.TryParse(_textMeshProtextInput.text.Substring(0, len - 1), out result));
        return result;
    }
    public string getInputName()
    {
        var len = _textMeshProtextInput.text.Length;
        return _textMeshProtextInput.text.Substring(0, len - 1);

    }

}
