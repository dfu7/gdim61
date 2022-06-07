using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float movementMultiplier = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    [SerializeField] private Transform orientation;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float crouchFOV = 50f;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float sprintFOV = 70f;
    [SerializeField] private float slideFOV = 75f;
    [SerializeField] private float fovTime = 4f;

    [Header("Sprinting")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float sprintAcceleration = 10f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 5f;

    [Header("Crouching")]
    [SerializeField] private float crouchHeightScale = 0.5f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float crouchAcceleration = 4f;

    [Header("Sliding")]
    [SerializeField] private float slideSpeed = 12f;
    [SerializeField] private float slideTime = 1f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.C;

    [Header("Drag")]
    [SerializeField] public float groundDrag = 6f;
    [SerializeField] public float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    private RaycastHit slopeHit;

    [Header("Physical Attributes")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform capsuleSize;
    [SerializeField] private CapsuleCollider playerCapsule;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;
    private Vector3 slopeMoveDirection;

    private float startPlayerHeight;
    private float currentPlayerHeight;

    public bool isCrouching { get; private set; }
    private bool isSprinting;
    private bool isSliding;

    //Controls the falling speed of the player
    public float fallMultiplier = 2.0f;

    

    private void Start()
    {
        rb.freezeRotation = true;
        startPlayerHeight = playerCapsule.height;
        currentPlayerHeight = startPlayerHeight;
        isCrouching = false;
        isSprinting = false;
        isSliding = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerInput();
        ControlDrag();
        ControlSpeed();
        ControlPhysical();

        if (Input.GetKeyDown(jumpKey) && isGrounded && !isCrouching)
        {
            Jump();
        }

        if (Input.GetKeyDown(crouchKey) && isGrounded && !isSprinting)
        {
            Crouch();
        }
        else if (Input.GetKeyDown(crouchKey) && isGrounded && isSprinting)
        {
            Slide();
            Invoke("stopSlide", slideTime);
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    public void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Crouch()
    { 
        if (!isCrouching)
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y * crouchHeightScale, 1);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y * crouchHeightScale, 0);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            isCrouching = true;
        }
        else
        {
            capsuleSize.localScale = new Vector3(1, capsuleSize.transform.localScale.y / crouchHeightScale, 1);
            transform.position = new Vector3(transform.position.x, groundCheck.position.y + capsuleSize.localScale.y, transform.position.z);
            cameraPosition.localPosition = new Vector3(0, cameraPosition.localPosition.y / crouchHeightScale, 0);
            isCrouching = false;
        }
    }

    private void Slide()
    {
        Crouch();
        isSliding = true;
    }

    private void stopSlide()
    {
        isSliding = false;
    }

    public void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, fovTime * Time.deltaTime);
            isSprinting = true;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, sprintAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
            isSprinting = false;
        }

        if (isCrouching && !isSliding)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, crouchAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, crouchFOV, fovTime * Time.deltaTime);
        }
        else if (isCrouching && isSliding)
        {
            moveSpeed = slideSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, slideFOV, fovTime * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, crouchAcceleration * Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFOV, fovTime * Time.deltaTime);
        }
    }

    private void ControlPhysical()
    {
        playerCapsule.height = currentPlayerHeight;
        groundCheck.localPosition = new Vector3(0, -capsuleSize.transform.localScale.y, 0);
    }

    public void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, currentPlayerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        //Controls the falling speed of the player
        if (rb.velocity.y <0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
       
        MovePlayer();
    }

    public void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }
}
