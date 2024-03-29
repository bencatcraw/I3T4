using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas pause;
    public Canvas gameOver;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName != "MainMenu" && Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause.enabled)
            {
                pause.enabled = false;
            }
            else
            {
                pause.enabled = true;
            }
        }
        if (sceneName != "MainMenu" && pause.enabled)
        {
            Time.timeScale = 0;
        }
        else if (gameOver == null || gameOver.enabled == false)
        {
            Time.timeScale = 1;
        }
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

}
