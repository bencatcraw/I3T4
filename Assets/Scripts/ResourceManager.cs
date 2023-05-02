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
    public int oreGet = 1;
    public float oreAmt = 5;
    public float oreMax = 5;
    public Image oreBar;
    public AudioSource pickup;
    public AudioSource mine;
    private void Start()
    {
        oreMax = oreAmt;
        player = GameObject.FindGameObjectWithTag("Player");
        mine = GameObject.Find("Mine").GetComponent<AudioSource>();
        pickup = GameObject.Find("Pickup").GetComponent<AudioSource>();
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            if(ResourceType == "Scrap")
            {
                Destroy(this.gameObject);
                player.GetComponent<PlayerController>().ScrapMetal += 1;
                pickup.Play();
            }
            if (ResourceType == "Titanium")
            {
                player.GetComponent<PlayerController>().Titanium += oreGet;
                oreAmt -= 1;
                mine.Play();
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

    private void OnTriggerEnter(Collider collision)
    {
        if(ResourceType == "Scrap" && collision.gameObject.tag == "Player")
        {
            pickup.Play();
            Destroy(this.gameObject);
            player.GetComponent<PlayerController>().ScrapMetal += 1;
            
        }
    }
}
