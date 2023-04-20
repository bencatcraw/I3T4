using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private bool inRange;
    private GameObject player;
    Animator animator;
    public float dist;
    bool closed = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {

            if (closed == false && other.gameObject.tag == "enemy")
            {
            animator.SetBool("resetting", false);
            animator.SetBool("triggered", true);
                closed = true;
                if (other.GetComponent<EnemyController>() != null) { other.GetComponent<EnemyController>().health -= 1000; }
                else if (other.GetComponent<RangedEnemy>() != null) { other.GetComponent<RangedEnemy>().health -= 1000; }

            }
        
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            if(closed == true)
            {
                animator.SetBool("resetting", true);
                animator.SetBool("triggered", false);
                closed = false;
                
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
