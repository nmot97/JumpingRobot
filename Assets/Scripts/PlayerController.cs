using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 10.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public GameObject robotPlayer;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    Animator animator;

    private static PlayerController playerInstance;

    void Awake(){ 
        if (playerInstance == null) {
            DontDestroyOnLoad (this);
            playerInstance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        animator = robotPlayer.GetComponent<Animator>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            animator.SetBool("Jumping", false);
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float xAxis = Input.GetAxis("Vertical");
            float yAxis = Input.GetAxis("Horizontal");

            float curSpeedX = speed * xAxis;
            float curSpeedY = speed * yAxis;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if(xAxis != 0 || yAxis != 0){
                animator.SetInteger("Speed", 2);
            }
            else{
                animator.SetInteger("Speed", 0);
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
			    animator.SetBool("Jumping", true);
            }
        }
        else{
            animator.SetBool("Jumping", false);
            animator.SetInteger("Speed", 0);
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);
        
    }

}
