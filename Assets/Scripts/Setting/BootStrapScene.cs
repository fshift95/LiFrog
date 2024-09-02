using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapScene : MonoBehaviour
{


    public void loadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
