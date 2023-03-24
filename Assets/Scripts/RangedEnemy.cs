using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RangedEnemy : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;

    public Transform target;
    private Vector3 moveDir;
    public Transform spawner;
    private LineRenderer renderer;

    public float maxHealth = 15.0f;
    public float health;
    public float maxDist = 5f;

    private float dist;

    public Image healthbar;
    private Animator animator;
    private void Start()
    {
        renderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, target.position);

            Vector3 direction = (target.position - transform.position);
            moveDir = direction;

            Movement();

        UpdateHealth(maxHealth, health);
        
    }
    void Movement()
    {  
        if(dist > maxDist){
            renderer.SetPosition(0, spawner.position);
            renderer.SetPosition(1, spawner.position);
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
        }
        else
        {
            Attack();
        }
    }
    void Attack()
    {
        renderer.SetPosition(0, spawner.position);
        renderer.SetPosition(1, target.transform.position);
    }
    public void UpdateHealth(float maxHealth, float health)
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        healthbar.fillAmount = health / maxHealth;
    }
}
