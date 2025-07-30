using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float movementSpeed = 50f;

    [SerializeField]
    private float jumpForce = 150f;

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

    private PlayerActions inputActions;
    private PlayerActions.MovementActions movementActions;

    private Rigidbody2D rb;
    private RaycastHit2D groundCastHit;
    private bool isGrounded;
    private bool isOnSlope;
    private bool isJumping;
    private bool canJump;
    private bool canWalkOnSlope;

    Vector2 targetVelocity;
    private Vector2 colliderSize;

    private float slopeDownAngle;
    private float slopeSideAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;

    [SerializeField]
    private PhysicsMaterial2D friction;

    [SerializeField]
    private PhysicsMaterial2D noFriction;

    private Vector2 moveDirection;

    private void Awake()
    {
        //Setup movement
        inputActions = new();
        movementActions = inputActions.Movement;

        rb = GetComponent<Rigidbody2D>();
        colliderSize = GetComponent<CapsuleCollider2D>().size;
    }

    private void Update()
    {
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

        if (isGrounded && !isJumping)
        {
            canJump = true;
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

        //Add forces to rigidbody
        //targetVelocity.Set(moveForce.x * (100 * Time.fixedDeltaTime), rb.linearVelocity.y);

        //On ground
        if (isGrounded && !isOnSlope && !isJumping)
        {
            targetVelocity.Set(moveForce.x, rb.linearVelocity.y);
            rb.linearVelocity = targetVelocity;
        }
        else if (isGrounded && isOnSlope && canWalkOnSlope && !isJumping)
        {
            targetVelocity.Set(
                -moveDirection.x * slopeNormalPerp.x * movementSpeed,
                movementSpeed * slopeNormalPerp.y * -moveDirection.x
            );
            rb.linearVelocity = targetVelocity;
        }
        else if (!isGrounded)
        {
            targetVelocity.Set(moveForce.x, rb.linearVelocity.y);
            rb.linearVelocity = targetVelocity;
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

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!canJump)
            return;

        canJump = false;
        isJumping = true;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce((100f * Time.deltaTime) * jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Gets the players input move direction
    /// </summary>
    /// <returns>A normalized movement direction</returns>
    Vector2 MoveDirection()
    {
        Vector2 moveDirection = movementActions.Move.ReadValue<Vector2>();

        //Sprite flipping
        if (moveDirection.x > 0)
        {
            //playerRenderer.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            //playerRenderer.flipX = true;
        }

        return moveDirection.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPosition.position, groundCheckRadius);
    }

    private void OnEnable()
    {
        inputActions.Enable();

        movementActions.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        movementActions.Jump.performed -= Jump;
    }
}
