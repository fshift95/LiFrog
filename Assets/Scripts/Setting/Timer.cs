using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timreText;
    [SerializeField] GameObject _pauseMenuContainer;
    public float _remainingTime;

    private float _startingTime;
    private void Start()
    {
        _startingTime = _remainingTime;
    }
    // Update is called once per frame
    void Update()
    {



        if (_remainingTime < 0)
        {
            _pauseMenuContainer.GetComponent<PauseMenu>().Pause();
            _timreText.text = "GAme Over";
        }
        else
        {
            _remainingTime -= Time.deltaTime;
            _timreText.text = _remainingTime.ToString();
            int minutes = Mathf.FloorToInt(_remainingTime / 60);
            int seconds = Mathf.FloorToInt(_remainingTime % 60);
            _timreText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


    }

    public void restartTimer()
    {
        _remainingTime = _startingTime;
    }
}
