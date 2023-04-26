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
    // Start is called before the first frame update
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
            Destroy(hbase);
        }
        healthbar.fillAmount = health / maxHealth;
    }
}
