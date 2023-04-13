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
    public float repairAmt = 5;
    public Turret turret;
    // Start is called before the first frame update
    private void Start()
    {
        heat = maxHeat;
        player = GameObject.FindGameObjectWithTag("Player");
        turret = this.gameObject.GetComponent<Turret>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateOverheat(maxHeat, heat);
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
    public void UpdateOverheat(float maxHeat, float heat)
    {
        if (heat <= 0)
        {
            turret.ableToShoot = false;
        }
        overheatBar.fillAmount = heat / maxHeat;
    }

}
