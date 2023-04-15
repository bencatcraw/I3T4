using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject Grid;
    public GameObject EnemySpawner;
    public static GameManager Instance;
    
    public GameState State;

    private ColorAdjustments col;
    public Volume volume;
    private void Awake()
    {
        Instance = this;
    }
    public static event Action<GameState> OnGameStateChanged;
    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.TryGet<ColorAdjustments>(out col))
        {
            col.postExposure.value = 1;
        }
        UpdateGameState(GameState.Morning);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(State == GameState.Morning) 
            {
                UpdateGameState(GameState.Afternoon);
                Grid.SetActive(true);
            }
            else if (State == GameState.Afternoon) 
            {
                UpdateGameState(GameState.Night);
                Grid.SetActive(false);
            }
        }
    }



    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Morning:
                    col.postExposure.value = 1;
                    EnemySpawner.SetActive(false);
                    break;
            case GameState.Afternoon:
                    col.postExposure.value = 0;
                    break;
            case GameState.Night:
                    col.postExposure.value = -3;
                    EnemySpawner.SetActive(true);
                    break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
    
    public enum GameState
    {
        Morning,
        Afternoon,
        Night,
        Lose
    }
}