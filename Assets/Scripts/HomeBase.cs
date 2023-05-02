using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HomeBase : MonoBehaviour
{
    public float health = 200f;
    public float maxHealth;
    public Image healthbar;
    

    public Canvas gameOver;
    [SerializeField] private GameObject hbase;

    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth(maxHealth, health);
    }

    public void UpdateHealth(float maxHealth, float health)
    {
        if (health <= 0)
        {
            gameOver.enabled = true;
            Time.timeScale = 0;
            Destroy(hbase);  
        }
        healthbar.fillAmount = health / maxHealth;
    }
    public void Repair()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player.GetComponent<PlayerController>().ScrapMetal >= 100)
        {
            player.GetComponent<PlayerController>().ScrapMetal -= 100;
            health = maxHealth;
        }
        
    }
}
