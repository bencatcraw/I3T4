using System;
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
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Morning)
        {
            resetTrap();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
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
                if (other.GetComponentInParent<EnemyController>()) { other.GetComponentInParent<EnemyController>().health -= 1000; }
                else if (other.GetComponentInParent<RangedEnemy>()) { other.GetComponentInParent<RangedEnemy>().health -= 1000; }

            }
        
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && inRange == true)
        {
            if(closed == true)
            {
                resetTrap();
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
    void resetTrap()
    {
        animator.SetBool("resetting", true);
        animator.SetBool("triggered", false);
        closed = false;
    }
}
