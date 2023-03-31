using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public string ResourceType;
    private bool inRange;
    private GameObject player;
    public float dist;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            if(ResourceType == "ScrapMetal")
            {
                player.GetComponent<PlayerController>().ScrapMetal += 1;
            }
            if (ResourceType == "Titanium")
            {
                player.GetComponent<PlayerController>().Titanium += 1;
            }
        }
    }
    private void Update()
    {
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
}
