using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 15f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 2.5f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);
   
    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //check standing on ground or not
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //reset the default velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Get the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //create the moving vector
        Vector3 move = transform.right * x + transform.forward * z; //right - red axis, forward - blue axis
        
        //move the player
        characterController.Move(move * speed * Time.deltaTime);

        //checking player jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //jump up
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //fall down
        velocity.y += gravity * Time.deltaTime;

        //execution of jump
        characterController.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true) 
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
    }
}
