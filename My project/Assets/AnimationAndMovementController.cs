using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    // declare referance variables
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    // variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    public float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 1.5f;
    public float speed = 7f;

    void Awake()
    {
        // Initially set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        // Set the player input callbacks
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
    }

    void onRun (InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        // The change in position our character should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        // the current rotation of our character
        Quaternion currentRotation = transform.rotation;

        // Creates a new rotation based on where the player is currently pressing

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    // Handler function to set the player input values
    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleAnimation()
    {
        // Get parameter values from animator
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // Start walking animation if movement pressed is true and not already walking
        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        // Stops walking animation if isMovementPressed is false and not already walking
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }

        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
   
    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();
        handleGravity();


        if (isRunPressed)
        { 
            characterController.Move(currentRunMovement * Time.deltaTime * speed);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime * speed);
        }
    }

    void OnEnable()
    {
        // Enable the character controls action map
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        // Disable the character controls action map
        playerInput.CharacterControls.Disable();
    }
}
