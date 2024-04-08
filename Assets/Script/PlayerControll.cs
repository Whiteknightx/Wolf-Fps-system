using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    //References
    [Header("References")]
    [HideInInspector] public Rigidbody rb;
    public Camera cam;

    [SerializeField] float moveSpeed;
    [SerializeField] float RunSpeed; // the run speed
    [SerializeField] float DefaultSpeed; // the walk speed
    [SerializeField] float smoothRot;


    float horInp;
    float verInp;
    [HideInInspector] public float moveINP;

    Vector3 camRelatedMovement;
    Vector3 camFront;
    Vector3 camRight;

    //Jump
    [Space(5)]
    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] Transform footPos;
    [SerializeField] float footradius;
    [SerializeField] float Gravitymultiplayar;
    public LayerMask whatIsGround;
    bool grounded;


    public float sprintMultiplar;


    //Jump And Anim
    public bool jumpUp;
    public bool falling;
    float jumpDelay;
    bool canCalculateJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
       // cam = Camera.main;
    }


    void Start()
    {
        DefaultSpeed = moveSpeed;
    }

    void Update()
    {
  
        jump();
   

    }


    private void FixedUpdate()
    {
        playerInput();
        

    }

   void playerInput()
   {
        horInp = Input.GetAxis("Horizontal");
        verInp = Input.GetAxis("Vertical");


        moveINP = Mathf.Abs(horInp) + Mathf.Abs(verInp);

        camFront = cam.transform.forward;
        camRight = cam.transform.right;
        camFront.y = 0;
        camRight.y = 0;

        Vector3 forwordMove = verInp * camFront;
        Vector3 rightMove = horInp * camRight;
        camRelatedMovement = forwordMove + rightMove;
        rb.AddForce(camRelatedMovement.normalized * moveSpeed * Time.deltaTime, ForceMode.Impulse);
   }


    void jump()
    {
        grounded = Physics.CheckSphere(footPos.position, footradius, whatIsGround);


        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpUp = true;

           
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(footPos.position, footradius);

    }

}
