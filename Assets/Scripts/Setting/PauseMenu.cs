
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject _timer;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _timer.GetComponent<Timer>().restartTimer();
        Time.timeScale = 1;

    }

    public void SaveQuest()
    {

        Time.timeScale = 0;
        var score = GameObject.Find("Player").GetComponent<ScoreController>().Score;

        // first burn the shot and then save the score...
        //GameObject.Find("MenuController").GetComponent<CallPubQuestSmartContract>().burnShotAndPlay();
        
        GameObject.Find("MenuController").GetComponent<CallPubQuestSmartContract>().saveScore(score);


    }
    public void SaveGame()
    {

        Time.timeScale = 0;
        var score = GameObject.Find("Player").GetComponent<ScoreController>().Score;
        GameObject.Find("MenuController").GetComponent<CallPlaySContract>().setScore(score);


    }
    public void SaveCroak()
    {

        Time.timeScale = 0;
        var score = GameObject.Find("Player").GetComponent<ScoreController>().Score;
        GameObject.Find("MenuController").GetComponent<CallCroakSmartContract>().saveScore(score);


    }
    private void Start()
    {
        isShow = false;
    }

    private bool isShow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isShow)
            {
                Pause();
                isShow = true;
            }
            else
            {
                Resume();
                isShow = false;
            }

        }


    }
}
