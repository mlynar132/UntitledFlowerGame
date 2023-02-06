using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody rb;
    private bool jumpKeyWasPressed;
    public float jumpForce;
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded;

    [SerializeField] public Transform GroundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    void Update()
    {

        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        { 
                jumpKeyWasPressed = true;
        
          
        }
        
    }

    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(GroundCheckTransform.position, 0.1f, playerMask).Length== 1)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpKeyWasPressed = false;
        }

        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput* moveSpeed);
    }

   
}

