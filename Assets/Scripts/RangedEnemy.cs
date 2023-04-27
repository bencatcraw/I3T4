using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource laserCharge;
    [SerializeField] private AudioSource laserShoot;

    public Rigidbody rb;
    public float moveSpeed;
    private bool moveable = true;
    private bool abletoShoot;

    public Transform target;
    private Vector3 moveDir;
    public Transform spawner;
    private LineRenderer renderer;
    public GameObject obstacle;
    public GameObject deadBody;

    public float maxHealth = 15.0f;
    public float health;
    public float maxDist = 5f;

    private float dist;
    public float fireRate = 5f;
    private float fireCount;

    public Image healthbar;
    private Animator animator;
    float animX;
    float animY;


    [SerializeField] private Transform downLeftSpawn;
    [SerializeField] private Transform downRightSpawn;
    [SerializeField] private Transform upLeftSpawn;
    [SerializeField] private Transform upRightSpawn;
    private void Start()
    {
        renderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    void Update()
    {

        renderer.SetPosition(0, spawner.position);
        dist = Vector3.Distance(transform.position, target.position);

        Vector3 direction = (target.position - transform.position).normalized;
        moveDir = direction;

        Movement();

        UpdateHealth(maxHealth, health);
        fireCount += Time.deltaTime;
        moveLaser();
    }
    void Movement()
    {
        if (moveable == true)
        {
            
            renderer.SetPosition(1, spawner.position);
            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
            if (dist < maxDist)
            {
                Attack();
            }
        }

        animator.SetFloat("X", rb.velocity.normalized.x);
        animator.SetFloat("Y", rb.velocity.normalized.z);
    }
    void Attack()
    {
        if (fireCount > fireRate)
        {
            animator.enabled = false;
            moveable = false;
            Vector3 pos = new Vector3(target.position.x, target.position.y - 8f, target.position.z);
            laserCharge.Play();
            renderer.SetPosition(1, pos);
            GameObject obstacleIns = Instantiate(obstacle, pos, Quaternion.identity);
            Destroy(obstacleIns, 2f);
            Invoke("stayStill", 2f);
            
        }

    }
    void moveLaser()
    {
        animX = animator.GetFloat("X");
        animY = animator.GetFloat("Y");
        if (animX < 0 && animY < 0)
        {
            spawner.position = downLeftSpawn.position;
        }
        if (animX > 0 && animY < 0)
        {
            spawner.position = downRightSpawn.position;
        }
        if (animX < 0 && animY > 0)
        {
            spawner.position = upLeftSpawn.position;
        }
        if (animX > 0 && animY > 0)
        {
            spawner.position = upRightSpawn.position;
        }
    }
    public void UpdateHealth(float maxHealth, float health)
    {
        if (health <= 0)
        {
            Instantiate(deadBody, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        healthbar.fillAmount = health / maxHealth;
    }
    public void stayStill()
    {
        fireCount = 0f;
        laserShoot.Play();
        renderer.SetPosition(1, spawner.position);

        moveable = true;
        animator.enabled = true;
    }

}
