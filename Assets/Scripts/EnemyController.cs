using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;
    public bool moveable = true;

    public Transform target;
    private Vector3 moveDir;

    public float maxHealth = 15.0f;
    public float health;
    public float damage = 10f;
    public float atkSpeed = 1f;

    private float atkTime;
    public Image healthbar;
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("base").transform;
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (moveable == true)
        {

            Vector3 direction = (target.position - transform.position).normalized;
            moveDir = direction;

            Movement();
        }
        UpdateHealth(maxHealth, health);
        atkTime += Time.deltaTime;
    }
    void Movement()
    {
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);
        animator.SetFloat("X", rb.velocity.normalized.x);
        animator.SetFloat("Y", rb.velocity.normalized.z);
    }
    public void UpdateHealth(float maxHealth, float health)
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        healthbar.fillAmount = health / maxHealth;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (atkTime > atkSpeed)
        {
            if (collision.gameObject.tag == "base")
            {
                target.GetComponent<HomeBase>().health -= damage;
            }
            atkTime = 0;
        }
    }
}
