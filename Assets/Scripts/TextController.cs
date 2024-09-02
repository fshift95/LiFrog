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
    [SerializeField] TextMeshProUGUI _textMeshProtextInput;

    public void setCornerText(string text)
    {
        _textMeshProCornerText.text = text;
    }
    public int getInputTextScore()
    {

        int result = 2;


        var len = _textMeshProtextInput.text.Length;
        Debug.LogError("textttttt iss   " + _textMeshProtextInput.text.Substring(0, len - 1));
        Debug.LogError("sdfsdfsdf" + int.TryParse(_textMeshProtextInput.text.Substring(0, len - 1), out result));
        return result;
    }

}
