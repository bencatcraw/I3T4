using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight, turHigh, trapHigh, defHigh, delHigh;
    public GameObject turret, trap;
    public int selected;
    private bool placed;
    private GameObject trapIns;
    private GameObject turretIns;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && selected != 1 && placed == false)
        {
            highlight.SetActive(false);
            selected = 1;
            highlight = turHigh;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            highlight.SetActive(false);
            selected = 0;
            highlight = defHigh;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && selected != 2 && placed == false)
        {
            highlight.SetActive(false);
            selected = 2;
            highlight = trapHigh;
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
            turretIns = Instantiate(turret, highlight.transform.position, highlight.transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selected = 0;
            highlight.SetActive(false);
            highlight = defHigh;
            highlight.SetActive(true);
            placed = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 2)
        {
            trapIns = Instantiate(trap, highlight.transform.position, highlight.transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selected = 0;
            highlight.SetActive(false);
            highlight = defHigh;
            highlight.SetActive(true);
            placed = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 3)
        {
            if(turretIns != null)
            {
                Destroy(turretIns);
            }
            else if (trapIns != null)
            {
                Destroy(trapIns);
            }

            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            placed = false;
        }

    }
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

}
