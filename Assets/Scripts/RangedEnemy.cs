using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RangedEnemy : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    private bool moveable = true;

    public Transform target;
    private Vector3 moveDir;
    public Transform spawner;
    private LineRenderer renderer;
    public GameObject obstacle;

    public float maxHealth = 15.0f;
    public float health;
    public float maxDist = 5f;

    private float dist;
    public float fireRate = 5f;
    private float fireCount;

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
        renderer.SetPosition(0, spawner.position);
        dist = Vector3.Distance(transform.position, target.position);

        Vector3 direction = (target.position - transform.position).normalized;
        moveDir = direction;

        if (moveable == true) { Movement(); }

        UpdateHealth(maxHealth, health);
        fireCount += Time.deltaTime;
    }
    void Movement()
    {
        if (dist > maxDist)
        {
            
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

        if (fireCount > fireRate)
        {
            Vector3 pos = new Vector3(target.position.x, 0.1f, target.position.z);
            
            renderer.SetPosition(1, pos);
            GameObject obstacleIns = Instantiate(obstacle, pos, Quaternion.identity);
            Destroy(obstacleIns, 5f);
            moveable = false;
            rb.isKinematic = true;
            Invoke("stayStill", 2f);
            fireCount = 0f;
        }
    }
    public void UpdateHealth(float maxHealth, float health)
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        healthbar.fillAmount = health / maxHealth;
    }
    public void stayStill()
    {
        
        renderer.SetPosition(1, spawner.position);
        rb.isKinematic = false;
        moveable = true;

    }

}
