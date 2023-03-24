using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight, turHigh, trapHigh , defHigh;
    public GameObject turret, trap;
    public int selected;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && selected != 1) 
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
        if (Input.GetKeyDown(KeyCode.Alpha2) && selected != 2)
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
    }
    private void OnMouseOver()
    {
        highlight.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R) && selected != 0){
            highlight.transform.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 1)
        {
            GameObject turretIns = Instantiate(turret, highlight.transform.position, highlight.transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && selected == 2)
        {
            GameObject trapIns = Instantiate(trap, highlight.transform.position, highlight.transform.rotation);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            
        }

    }
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    
}
