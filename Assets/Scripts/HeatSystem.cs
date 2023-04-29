using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeatSystem : MonoBehaviour
{
    public Image overheatBar;
    public float dist;
    private bool inRange = false;
    private GameObject player;
    public float maxHeat = 100;
    public float heat;
    public float repairAmt;
    public Turret turret;
    private Upgrades upgrades;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Morning)
        {
            heat = maxHeat;
        }
    }
    private void Start()
    {
        upgrades = GameObject.FindGameObjectWithTag("GameController").GetComponent<Upgrades>();
        heat = maxHeat;
        player = GameObject.FindGameObjectWithTag("Player");
        turret = this.gameObject.GetComponent<Turret>();
    }

    // Update is called once per frame
    private void Update()
    {
        maxHeat = upgrades.turretMaxHeat;
        repairAmt = player.GetComponent<PlayerController>().repairAmt;
        UpdateOverheat();
        dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= 30)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
        
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            heat += repairAmt;
        }
    }
    public void UpdateOverheat()
    {
        if(heat > maxHeat)
        {
            heat = maxHeat;
        }
        overheatBar.fillAmount = heat / maxHeat;
    }

}
