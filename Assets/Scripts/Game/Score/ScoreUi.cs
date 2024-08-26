using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUi : MonoBehaviour
{

    private TMP_Text _scoreText;
    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();

    }

    public void UpdateScore(ScoreController _scoreController)
    {
        _scoreText.text = $"Score : {_scoreController.Score}";
    }
}
