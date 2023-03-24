using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    private ColorAdjustments col;
    public Volume volume;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out col)){
            col.postExposure.value = 0;
            
        }
        UpdateGameState(GameState.Morning);
        
    }
    private void FixedUpdate()
    {
        col.postExposure.value = Mathf.Lerp(0, -3, Time.time / 100);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.PauseScreen:
                break;
            case GameState.Morning:
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
}