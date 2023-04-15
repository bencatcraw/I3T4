using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public Canvas screen;
    private bool isPaused;

    private ColorAdjustments col;
    public Volume volume;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // like Minecraft, 0 ticks means it's dawn
        if (volume.profile.TryGet<ColorAdjustments>(out col))
        {
            col.postExposure.value = 0;

        }
        // game state starts in the morning
        UpdateGameState(GameState.Morning);
        // pause screen is not visible at the start of game
        isPaused = false;
        screen.enabled = false;
    }
    // made Update function so that the pause screen would actually work
    void Update() 
    {
        // toggle pause screen
        if (Input.GetKeyDown(KeyCode.Q)) {
            
            if (!isPaused)   { 
                UpdateGameState(GameState.PauseScreen); 
                isPaused = true;
            } else { 
                UpdateGameState(GameState.Morning);
                isPaused = false; 
            }
        }
    }
    private void FixedUpdate()
    {
        // changes from day time to night time in a long while
        col.postExposure.value = Mathf.Lerp(0, -3, Time.time / 100);
        // current state is Morning
        Debug.Log(State);

        
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.PauseScreen:
                // when pause screen is enabled, time stops
                screen.enabled = true;
                Time.timeScale = 0f;
                break;
            case GameState.Morning:
                // when pause screen is disabled, time resumes
                screen.enabled = false;
                Time.timeScale = 1f;
                break;
            case GameState.Afternoon:
                break;
            case GameState.NightFight:
                break;
            case GameState.NightCollect:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
    public enum GameState
    {
        PauseScreen,
        Morning,
        Afternoon,
        NightFight,
        NightCollect,
        Lose
    }
    public void ResumeGame()
    {
        UpdateGameState(GameState.Morning);
        isPaused = false;

        /*
            // if we use this code, then the pause functionality may not be consistent
            // when pause screen is disabled, time resumes
            screen.enabled = false;
            Time.timeScale = 1f;
        */
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame() 
    {
        SceneManager.LoadScene("ben test");
    }
}