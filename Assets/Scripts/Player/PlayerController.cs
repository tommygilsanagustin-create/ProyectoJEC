using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Crouch")]
    public float crouchHeightMultiplier = 0.5f;
    public LayerMask ceilingLayer;

    [Header("Climb")]
    public float climbSpeed = 4f;
    private bool isClimbing;

    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    [Header("Checks")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int jumpCount;

    public int facingDirection = 1; // 1 derecha, -1 izquierda
    private float lastDirection = 1f;
    private float moveInput;

    public PlayerState currentState;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
        HandleState();

        if (isClimbing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopClimbing();
                Jump();
            }
            return;
        }

        Move(moveInput);
    }

    void Flip(float direction)
    {
        if (direction == 0) return;

        lastDirection = direction;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (direction > 0 ? 1 : -1);
        transform.localScale = scale;
    }

    public void SetMoveInput(float value)
    {
        moveInput = value;

        if (value != 0)
            Flip(value);
    }

    public bool CanMove()
    {
        return currentState != PlayerState.Dead &&
               currentState != PlayerState.Climb;
    }

    public bool CanJump()
    {
        return currentState != PlayerState.Crouch &&
               currentState != PlayerState.Dead &&
               currentState != PlayerState.Climb;
    }

    public bool CanShoot()
    {
        return currentState != PlayerState.Crouch &&
               currentState != PlayerState.Dead &&
               currentState != PlayerState.Climb;
    }

    public void Move(float direction)
    {
        if (!CanMove()) return;

        float speed = moveSpeed;

        // Si está en Crouch se mueve lento automáticamente
        if (currentState == PlayerState.Crouch)
        {
            speed *= 0.5f;
            direction = facingDirection; // Se mueve hacia donde mira
        }
        else
        {
            if (direction != 0)
                facingDirection = direction > 0 ? 1 : -1;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (!isGrounded) return;

        if (currentState != PlayerState.Crouch)
        {
            if (direction != 0)
                currentState = PlayerState.Run;
            else
                currentState = PlayerState.Idle;
        }
    }

    public void Jump()
    {
        if (!CanJump()) return;
        if (jumpCount >= 1) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpCount++;
        currentState = PlayerState.Jump;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );

        if (isGrounded)
            jumpCount = 0;

        if (isGrounded && currentState == PlayerState.Jump)
            currentState = PlayerState.Idle;
    }

    void HandleState()
    {
        animator.SetBool("isRunning", currentState == PlayerState.Run);
        animator.SetBool("isJumping", currentState == PlayerState.Jump);
        animator.SetBool("isCrouching", currentState == PlayerState.Crouch);
        animator.SetBool("isClimbing", currentState == PlayerState.Climb);
        animator.SetBool("isDead", currentState == PlayerState.Dead);
    }

    // =========================
    // CROUCH
    // =========================

    public void StartCrouch()
    {
        if (!isGrounded) return;
        if (currentState == PlayerState.Crouch) return;

        currentState = PlayerState.Crouch;

        boxCollider.size = new Vector2(
            originalColliderSize.x,
            originalColliderSize.y * crouchHeightMultiplier
        );

        boxCollider.offset = new Vector2(
            originalColliderOffset.x,
            originalColliderOffset.y - (originalColliderSize.y * 0.25f)
        );
    }

    public void StopCrouch()
    {
        if (CanStandUp())
        {
            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
            currentState = PlayerState.Idle;
        }
    }

    bool CanStandUp()
    {
        Vector2 checkPosition = transform.position + Vector3.up * originalColliderSize.y;
        return !Physics2D.OverlapCircle(checkPosition, 0.2f, ceilingLayer);
    }

    // =========================
    // CLIMB
    // =========================

    public void StartClimbing()
    {
        if (currentState == PlayerState.Dead) return;

        currentState = PlayerState.Climb;
        isClimbing = true;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

    public void ClimbMove(float vertical)
    {
        if (!isClimbing) return;

        rb.linearVelocity = new Vector2(0f, vertical * climbSpeed);
    }

    public void StopClimbing()
    {
        if (!isClimbing) return;

        isClimbing = false;
        rb.gravityScale = 3f;
        currentState = PlayerState.Idle;
    }

    // =========================
    // DEATH
    // =========================

    public void Die()
    {
        if (currentState == PlayerState.Dead) return;

        currentState = PlayerState.Dead;
        rb.linearVelocity = Vector2.zero;

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif

        Invoke(nameof(NotifyDeath), 0.4f);
    }

    void NotifyDeath()
    {
        GameManager.Instance.PlayerDied(gameObject);
    }

    public void Respawn()
    {
        currentState = PlayerState.Idle;
        rb.linearVelocity = Vector2.zero;
    }
}