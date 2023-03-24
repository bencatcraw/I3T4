using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed;

    public Vector2 moveInput;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "turret")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                collision.gameObject.GetComponentInChildren<Turret>().heat += 1;
            }
        }
    }
}
