using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public GameObject upgradeButton;
    private GameObject player;

    public float turretMaxHeat = 100f;
    public float turretFireRate = 1f;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Morning)
        {
            upgradeButton.SetActive(true);
        }
        if (state == GameManager.GameState.Night)
        {
            upgradeButton.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void UpgradeRepairAmt()
    {
        if(player.GetComponent<PlayerController>().ScrapMetal >= 25)
        {
            player.GetComponent<PlayerController>().ScrapMetal -= 25;
            player.GetComponent<PlayerController>().repairAmt += 2;
        }
    }
    public void UpgradeHeat()
    {
        if (player.GetComponent<PlayerController>().ScrapMetal >= 50)
        {
            player.GetComponent<PlayerController>().ScrapMetal -= 50;
            turretMaxHeat += 10;
            GameObject[] currentTurrets = GameObject.FindGameObjectsWithTag("turret");
            foreach (var turret in currentTurrets)
            {
                turret.GetComponent<HeatSystem>().heat = turretMaxHeat;
            }
        }
    }
    public void UpgradeFireRate()
    {
        if (player.GetComponent<PlayerController>().ScrapMetal >= 75)
        {
            player.GetComponent<PlayerController>().ScrapMetal -= 75;
            turretFireRate += 0.2f;
        }
    }
}
