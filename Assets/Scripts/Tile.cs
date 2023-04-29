using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight, turHigh, trapHigh, defHigh, delHigh;
    public GameObject turret, trap;
    public int selected;
    private bool isPlaced;
    private GameObject trapIns;
    private GameObject turretIns;
    private GameObject player;

    public int TurrCostTitanium = 10;
    public int TurrCostScrap = 5;
    public int TrapCostTitanium = 5;
    public int TrapCostScrap = 2;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && selected != 1)
        {
            if (isPlaced == false)
            {
                highlight.SetActive(false);
                selected = 1;
                highlight = turHigh;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            highlight.SetActive(false);
            selected = 0;
            highlight = defHigh;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && selected != 2 )
        {
            if(isPlaced == false)
            {
                highlight.SetActive(false);
                selected = 2;
                highlight = trapHigh;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            highlight.SetActive(false);
            selected = 0;
            highlight = defHigh;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && selected != 3)
        {
            highlight.SetActive(false);
            selected = 3;
            highlight = delHigh;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            highlight.SetActive(false);
            selected = 0;
            highlight = defHigh;
        }


    }
    private void OnMouseOver()
    {
        highlight.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R) && selected != 0)
        {
            highlight.transform.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 1)
        {
            if (player.GetComponent<PlayerController>().Titanium >= TurrCostTitanium && player.GetComponent<PlayerController>().ScrapMetal >= TurrCostScrap) 
            {
                
                player.GetComponent<PlayerController>().Titanium -= TurrCostTitanium;
                player.GetComponent<PlayerController>().ScrapMetal -= TurrCostScrap;
                turretIns = Instantiate(turret, highlight.transform.position, highlight.transform.rotation);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selected = 0;
                highlight.SetActive(false);
                highlight = defHigh;
                highlight.SetActive(true);
                isPlaced = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 2)
        {
            if (player.GetComponent<PlayerController>().Titanium >= TrapCostTitanium && player.GetComponent<PlayerController>().ScrapMetal >= TrapCostScrap)
            {
                isPlaced = true;
                player.GetComponent<PlayerController>().Titanium -= TrapCostTitanium;
                player.GetComponent<PlayerController>().ScrapMetal -= TrapCostScrap;
                trapIns = Instantiate(trap, highlight.transform.position, highlight.transform.rotation);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selected = 0;
                highlight.SetActive(false);
                highlight = defHigh;
                highlight.SetActive(true);
                isPlaced = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 3 && isPlaced == true)
        {
            if(turretIns != null)
            {
                player.GetComponent<PlayerController>().Titanium += TurrCostTitanium;
                player.GetComponent<PlayerController>().ScrapMetal += TurrCostScrap;
                Destroy(turretIns);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isPlaced = false;
            }
            else if (trapIns != null)
            {
                player.GetComponent<PlayerController>().Titanium += TrapCostTitanium;
                player.GetComponent<PlayerController>().ScrapMetal += TrapCostScrap;
                Destroy(trapIns);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isPlaced = false;
            }

            
        }
        if (turretIns == null && trapIns == null)
        {
            if (isPlaced)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isPlaced = false;
            }
            
        }

    }
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

}
