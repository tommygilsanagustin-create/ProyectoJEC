using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Crawl")]
    public float crawlHeightMultiplier = 0.5f;
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
    public int facingDirection = 1; // 1 = derecha, -1 = izquierda
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
        return currentState != PlayerState.Crawl &&
            currentState != PlayerState.Dead &&
            currentState != PlayerState.Climb;
    }

    public bool CanShoot()
    {
        return currentState != PlayerState.Crawl &&
            currentState != PlayerState.Dead &&
            currentState != PlayerState.Climb;
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

    public void Move(float direction)
    {
        if (!CanMove()) return;

        if (direction != 0)
            facingDirection = direction > 0 ? 1 : -1;

        float speed = currentState == PlayerState.Crawl 
            ? moveSpeed * 0.5f 
            : moveSpeed;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (!isGrounded) return;

        if (direction != 0 && currentState != PlayerState.Crawl)
            currentState = PlayerState.Run;
        else if (currentState != PlayerState.Crawl)
            currentState = PlayerState.Idle;
    }


    public void Jump()
    {
        if (!CanJump()) return;
        if (jumpCount >= 2) return;

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
    }

    public void StartCrawl()
    {
        if (!isGrounded) return;
        if (currentState == PlayerState.Crawl) return;

        currentState = PlayerState.Crawl;

        boxCollider.size = new Vector2(
            originalColliderSize.x,
            originalColliderSize.y * crawlHeightMultiplier
        );

        boxCollider.offset = new Vector2(
            originalColliderOffset.x,
            originalColliderOffset.y - (originalColliderSize.y * 0.25f)
        );
    }

    public void StopCrawl()
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

        if (vertical != 0)
            currentState = PlayerState.Climb;
    }


    public void StopClimbing()
    {
        if (!isClimbing) return;

        isClimbing = false;
        rb.gravityScale = 3f; // o tu valor original
        currentState = PlayerState.Idle;
    }



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