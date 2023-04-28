using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceManager : MonoBehaviour
{
    public string ResourceType;
    private bool inRange;
    private GameObject player;
    public float dist;
    public float oreAmt;
    public float oreMax;
    public Image oreBar;
    private void Start()
    {
        oreMax = oreAmt;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            if(ResourceType == "Scrap")
            {
                Destroy(this.gameObject);
                player.GetComponent<PlayerController>().ScrapMetal += 1;
            }
            if (ResourceType == "Titanium")
            {
                player.GetComponent<PlayerController>().Titanium += 1;
                oreAmt -= 1;
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
        if (ResourceType == "Titanium") { UpdateOreBar(oreAmt, oreMax); }
    }

    public void UpdateOreBar(float curOre, float maxOre)
    {
        if (curOre <= 0)
        {

            transform.gameObject.SetActive(false);
        }
        else
        oreBar.fillAmount = curOre / maxOre;
    }
}
