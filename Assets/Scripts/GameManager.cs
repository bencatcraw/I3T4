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
    private GameObject[] ores;
    private void Awake()
    {
        Instance = this;
    }
    public static event Action<GameState> OnGameStateChanged;
    // Start is called before the first frame update
    void Start()
    {
        ores = GameObject.FindGameObjectsWithTag("Titanium");
        if (volume.profile.TryGet<ColorAdjustments>(out col))
        {
            col.postExposure.value = 1;
        }
        UpdateGameState(GameState.Morning);

    }
    void Update()
    {   if (EnemySpawner.GetComponent<WaveSpawner>().startedSpawning == true)
        {if(EnemySpawner.transform.childCount <= 0)
            {
                UpdateGameState(GameState.Morning);
                EnemySpawner.GetComponent<WaveSpawner>().startedSpawning = false;
            }
            
           
        }
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
                foreach (GameObject ore in ores)
                {
                    ore.SetActive(true);
                    ore.GetComponent<ResourceManager>().oreMax += 2;
                    ore.GetComponent<ResourceManager>().oreAmt = ore.GetComponent<ResourceManager>().oreMax;
                }
                col.postExposure.value = 1;
                    EnemySpawner.SetActive(false);
                    break;
            case GameState.Afternoon:
                    col.postExposure.value = 0;
                    break;
            case GameState.Night:
                    col.postExposure.value = -2;
                    EnemySpawner.SetActive(true);
                    EnemySpawner.GetComponent<WaveSpawner>().Invoke("increaseNight", 0);
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