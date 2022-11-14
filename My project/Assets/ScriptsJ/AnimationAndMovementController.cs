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
    private GameMaster gm;

    // variables to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;

    // variables to store player input values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;

    // Constants
    public float rotationFactorPerFrame = 15.0f;
    float runMultiplier = 15f;
    public float speed = 10f;
    int zero = 0;

    // gravity variables
    public float gravity = -9.8f;
    float groundedGravity = -.05f;

    // Jumping variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 2.2f;
    public float maxJumpTime = 0.8f;
    public float secondJumpMod = 2f;
    public float thirdJumpMod = 4f;
    public float secondJumpTime = 1.25f;
    public float thirdJumpTime = 1.5f;
    bool isJumping = false;
    int isJumpingHash;
    int jumpCountHash;
    bool isJumpAnimating = false;
    int jumpCount = 0;
    Dictionary<int, float> initialJumpVelocities = new Dictionary<int, float>();
    Dictionary<int, float> jumpGravities = new Dictionary<int, float>();
    Coroutine currentJumpResetRoutine = null;

    void Awake()
    {
        // Initially set reference variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        // Set the parameter hash references 
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        jumpCountHash = Animator.StringToHash("jumpCount");

        // Set the player input callbacks
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        this.transform.SetPositionAndRotation(new Vector3(gm.lastCheckPointPos.x, gm.lastCheckPointPos.y, 0), new Quaternion(0f,90f,0f,0f));

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        float secondJumpGravity = (-2 * (maxJumpHeight + secondJumpMod)) / Mathf.Pow((timeToApex * secondJumpTime), 2);
        float secondJumpInitialVelocity = (2 * (maxJumpHeight + secondJumpMod)) / (timeToApex * secondJumpTime);
        float thirdJumpGravity = (-2 * (maxJumpHeight + thirdJumpMod)) / Mathf.Pow((timeToApex * thirdJumpTime), 2);
        float thirdJumpInitialVelocity = (2 * (maxJumpHeight + thirdJumpMod)) / (timeToApex * thirdJumpTime);

        initialJumpVelocities.Add(1, initialJumpVelocity);
        initialJumpVelocities.Add(2, secondJumpInitialVelocity);
        initialJumpVelocities.Add(3, thirdJumpInitialVelocity);

        jumpGravities.Add(0, gravity);
        jumpGravities.Add(1, gravity);
        jumpGravities.Add(2, secondJumpGravity);
        jumpGravities.Add(3, thirdJumpGravity);

    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            if (jumpCount < 3 && currentJumpResetRoutine != null)
            {
                StopCoroutine(currentJumpResetRoutine);
            }
            animator.SetBool(isJumpingHash, true);
            isJumpAnimating = true;
            isJumping = true;
            jumpCount += 1;
            animator.SetInteger(jumpCountHash, jumpCount);
            currentMovement.y = initialJumpVelocities[jumpCount];
            appliedMovement.y = initialJumpVelocities[jumpCount];
        } else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    IEnumerator jumpResetRoutine()
    {
        yield return new WaitForSeconds(.5f);
        jumpCount = 0;
    }

    void onJump (InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
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
        positionToLookAt.z = 0.0f;
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
        currentMovement.x = currentMovementInput.x * speed;
        //currentMovement.z = currentMovementInput.y * speed;
        currentMovement.z = 0;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        //currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != zero;

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
        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;
        float fallMultiplier = 2.0f;
        // Apply proper gravity if the player is grounded or not
        if (characterController.isGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
                currentJumpResetRoutine = StartCoroutine(jumpResetRoutine());
                if (jumpCount == 3)
                {
                    jumpCount = 0;
                    animator.SetInteger(jumpCountHash, jumpCount);
                }
            }
            
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;

            // Additional gravity applied after reaching apex of jump
        } else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (jumpGravities[jumpCount] * fallMultiplier * Time.deltaTime);
            appliedMovement.y = Mathf.Max((previousYVelocity + currentMovement.y) * .5f, -20.0f);       
        }
        // applied when character is not grounded
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (jumpGravities[jumpCount] * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * .5f;           
        }
    }

    void LateUpdate()
    {
        if (this.transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        handleAnimation();

        
        


        if (isRunPressed)
        {
            appliedMovement.x = currentRunMovement.x;
            
        }
        else
        {
            appliedMovement.x = currentMovement.x;
          
        }


        characterController.Move(appliedMovement * Time.deltaTime);

        handleGravity();
        handleJump();
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
