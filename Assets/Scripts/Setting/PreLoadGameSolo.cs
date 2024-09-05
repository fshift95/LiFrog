using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoadGameSolo : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI _PlayerScore;

    private void Start()
    {
        GetComponent<CallPlaySContract>().getScore();
    }

    public void setScoreText(string text)
    {
        _PlayerScore.text =  text;
    }
}
