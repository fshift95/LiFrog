
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
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
        Time.timeScale = 1;

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
