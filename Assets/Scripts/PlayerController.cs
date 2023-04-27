using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource dieSound;

    public Rigidbody rb;
    public float moveSpeed;

    public Vector2 moveInput;

    private Animator animator;

    public int Titanium = 0;
    public int ScrapMetal = 0;

    public float PlayerHealth = 100;

    public SpriteRenderer rendere;
    public Canvas gameOver;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            Time.timeScale = 0;
            rendere.enabled = false;
            gameOver.enabled = true;

        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walkingSound.UnPause();
        } else
        {
            walkingSound.Pause();
        }
        
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        //moveInput.Normalize();

        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector3(moveInput.x * moveSpeed, rb.velocity.y, moveInput.y * moveSpeed);
        
        animator.SetFloat("X", moveInput.x);
        animator.SetFloat("Y", moveInput.y);
    }
    void Hurt()
    {
        if (PlayerHealth <= 0)
        {
            dieSound.Play();
        }
        else
        {
            hurtSound.Play();
        }
            
    }

}
