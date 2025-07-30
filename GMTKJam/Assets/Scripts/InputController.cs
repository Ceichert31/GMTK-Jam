using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class InputController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float movementSpeed = 50f;

    [SerializeField]
    private float jumpForce = 150f;

    [SerializeField]
    private float groundCastLength = 1f;

    [SerializeField]
    private LayerMask groundLayer;

    private PlayerActions inputActions;
    private PlayerActions.MovementActions movementActions;

    private Rigidbody2D rb;
    private RaycastHit2D groundCastHit;
    private bool isGrounded;
    private bool isOnSlope;

    Vector2 targetVelocity;
    private Vector2 colliderSize;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    private Vector2 slopeNormalPerp;

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
        groundCastHit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCastLength,
            groundLayer
        );

        isGrounded = groundCastHit;

        //Debug.DrawRay(transform.position, Vector2.down * groundCastLength, Color.red);
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
        Vector2 moveForce = movementSpeed * MoveDirection();

        //Add forces to rigidbody
        targetVelocity.Set(moveForce.x * (100 * Time.fixedDeltaTime), rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;

        if (isGrounded && !isOnSlope)
        {
            targetVelocity.Set(moveForce.x, 0.0f);
            rb.linearVelocity = targetVelocity;
        }
        else if (isGrounded && isOnSlope) { }
        else if (!isGrounded)
        {
            targetVelocity.Set(moveForce.x, rb.linearVelocity.y);
            rb.linearVelocity = targetVelocity;
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0f, colliderSize.y / 2);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos) { }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, groundCastLength, groundLayer);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal);

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!isGrounded)
            return;
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
