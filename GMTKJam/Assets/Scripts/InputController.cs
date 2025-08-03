using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float movementSpeed => StatsManager.instance.currentPlayerStats.speed;

    [SerializeField]
    private float slopeSpeed = 10f;

    [SerializeField]
    private float jumpForce => StatsManager.instance.currentPlayerStats.jumpForce;

    [SerializeField]
    private float slopeCheckLength = 1f;

    [SerializeField]
    private Transform groundCheckPosition;

    [SerializeField]
    private Transform slopeCheckPosition;

    [SerializeField]
    private float maxSlopeAngle = 65f;

    [SerializeField]
    private float groundCheckRadius = 0.5f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private PhysicsMaterial2D friction;

    [SerializeField]
    private PhysicsMaterial2D noFriction;

    [SerializeField]
    private ParticleSystem speedParticle;

    [SerializeField]
    private float speedParticleShowThreshold = 5f;

    private PlayerActions inputActions;
    private PlayerActions.MovementActions movementActions;

    private Vector2 targetVelocity;
    private Vector2 moveDirection;

    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canJump;
    private bool canWalkOnSlope;

    private float slopeDownAngle;
    private float slopeSideAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;

    private Rigidbody2D rb;

    private Transform spriteObject;
    private SpriteRenderer playerRenderer;

    //private MP3Controller mp3Controller;
    private Animator playerAnimator;

    [SerializeField]
    private AudioSource sfxPlayer;

    [SerializeField]
    private AudioClip jumpSFX;

    [SerializeField]
    private float coyoteTime = 0.2f;
    private float coyoteTimer;

    private void Awake()
    {
        //Setup movement
        inputActions = new();
        movementActions = inputActions.Movement;

        rb = GetComponent<Rigidbody2D>();
        spriteObject = transform.GetChild(0);
        playerRenderer = spriteObject.GetComponent<SpriteRenderer>();

        playerAnimator = GetComponent<Animator>();

        //mp3Controller = transform.GetComponentInChildren<MP3Controller>();
    }

    private Vector2 previousPosition;

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        moveDirection = movementActions.Move.ReadValue<Vector2>().normalized;

        isGrounded = Physics2D.OverlapCircle(
            groundCheckPosition.position,
            groundCheckRadius,
            groundLayer
        );

        if (rb.linearVelocity.y <= 0)
        {
            isJumping = false;
        }

        //Check last position and compare to current position to determine if falling

        if (transform.position.y < previousPosition.y)
        {
            playerAnimator.SetBool("IsFalling", true);
        }
        previousPosition = transform.position;

        if (isGrounded && !isJumping)
        {
            canJump = true;
            playerAnimator.SetBool("IsFalling", false);
        }

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (coyoteTimer > 0f && !isJumping)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        //Sprite flipping
        if (moveDirection.x > 0)
        {
            playerRenderer.flipX = false;
            playerAnimator.SetBool("IsWalking", true);
            //spriteObject.DORotate(new Vector3(0, 0, -6f), 1f);
        }
        else if (moveDirection.x < 0)
        {
            playerRenderer.flipX = true;
            playerAnimator.SetBool("IsWalking", true);
            //spriteObject.DORotate(new Vector3(0, 0, 6f), 1f);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            //Change sprite to face camera
            //spriteObject.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }

    private void FixedUpdate()
    {
        SlopeCheck();
        Move();
    }

    /// <summary>
    /// Applies forces to player based on input
    /// </summary>
    private void Move()
    {
        Vector2 moveForce = (100 * Time.deltaTime) * movementSpeed * moveDirection;

        if (moveDirection != Vector2.zero && TutorialController.Instance.tutorial1)
        {
            TutorialController.Instance.tutorial1 = true;
            TutorialController.Instance.walking.SetActive(false);
            TutorialController.Instance.jump.SetActive(true);
        }

        //On ground
        if (isGrounded && !isOnSlope && !isJumping)
        {
            targetVelocity.Set(moveForce.x, rb.linearVelocity.y);
            rb.linearVelocity = targetVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping)
        {
            targetVelocity.Set(
                slopeSpeed * slopeNormalPerp.x * -moveDirection.x,
                slopeSpeed * slopeNormalPerp.y * -moveDirection.x
            );

            Debug.DrawRay(transform.position, targetVelocity.normalized, Color.coral);
            rb.linearVelocity = targetVelocity;
        }
        else if (!isGrounded)
        {
            targetVelocity.Set(moveForce.x, rb.linearVelocity.y);
            rb.linearVelocity = targetVelocity;
        }

        if (rb.linearVelocity.magnitude >= speedParticleShowThreshold)
        {
            speedParticle.Play();
        }
        else
        {
            speedParticle.Stop();
        }
    }

    private void SlopeCheck()
    {
        SlopeCheckHorizontal(slopeCheckPosition.position);
        SlopeCheckVertical(slopeCheckPosition.position);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(
            checkPos,
            transform.right,
            slopeCheckLength,
            groundLayer
        );
        RaycastHit2D slopeHitBack = Physics2D.Raycast(
            checkPos,
            -transform.right,
            slopeCheckLength,
            groundLayer
        );

        if (slopeHitFront)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;

            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

        Debug.DrawRay(checkPos, transform.right * slopeCheckLength, Color.blue);
        Debug.DrawRay(checkPos, -transform.right * slopeCheckLength, Color.yellow);
    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckLength, groundLayer);
        Debug.DrawRay(checkPos, Vector2.down * slopeCheckLength, Color.brown);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && canWalkOnSlope && moveDirection.x == 0.0f)
        {
            rb.sharedMaterial = friction;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    [SerializeField]
    private float jumpAnimationSpeed = 0.3f;

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0)
            return;

        if (!canJump)
            return;

        if (TutorialController.Instance.tutorial2)
        {
            TutorialController.Instance.tutorial2 = true;
            TutorialController.Instance.jump.SetActive(false);
        }

        canJump = false;
        isJumping = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);

        playerAnimator.SetTrigger("Jump");
        sfxPlayer.PlayOneShot(jumpSFX);

        //var jumpTween = spriteObject.DOScaleY(1.4f, jumpAnimationSpeed).SetEase(Ease.Flash);
        //await jumpTween.AsyncWaitForCompletion();
        //spriteObject.DOScaleY(1.7f, jumpAnimationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }

    private void OnEnable()
    {
        inputActions.Enable();

        movementActions.Jump.performed += Jump;

        //Mp3 controls
        //movementActions.Next.performed += (ctx) => mp3Controller.SkipForward();
        //movementActions.Previous.performed += (ctx) => mp3Controller.SkipBackward();
        //movementActions.Pause.performed += (ctx) => mp3Controller.Pause();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        movementActions.Jump.performed -= Jump;

        //Mp3 Controls
        // movementActions.Next.performed -= (ctx) => mp3Controller.SkipForward();
        //movementActions.Previous.performed -= (ctx) => mp3Controller.SkipBackward();
        //movementActions.Pause.performed -= (ctx) => mp3Controller.Pause();
    }
}
