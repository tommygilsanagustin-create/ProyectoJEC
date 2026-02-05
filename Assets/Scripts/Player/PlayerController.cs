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

    public PlayerState currentState;


    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    public bool CanMove()
    {
        return currentState != PlayerState.Dead;
    }

    public bool CanJump()
    {
        return currentState != PlayerState.Crawl &&
            currentState != PlayerState.Dead;
    }

    public bool CanShoot()
    {
        return currentState != PlayerState.Crawl &&
            currentState != PlayerState.Dead;
    }


    void Update()
    {
        CheckGround();
        HandleState();
    }

    public void Move(float direction)
    {
        if (!CanMove()) return;

        float speed = currentState == PlayerState.Crawl 
            ? moveSpeed * 0.5f 
            : moveSpeed;

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if (!isGrounded) return;

        if (direction != 0 && currentState != PlayerState.Crawl)
            currentState = PlayerState.Run;
        else if (currentState != PlayerState.Crawl)
            currentState = PlayerState.Idle;
    }



    public void Jump()
    {
        if (currentState == PlayerState.Crawl) return;
        if (jumpCount >= 2) return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        {
            currentState = PlayerState.Idle;
        }

    }

    void HandleState()
    {
        animator.SetBool("isRunning", currentState == PlayerState.Run);
        animator.SetBool("isJumping", currentState == PlayerState.Jump);
        animator.SetBool("isCrawling", currentState == PlayerState.Crawl);
        animator.SetBool("isDead", currentState == PlayerState.Dead);

        animator.SetFloat("verticalSpeed", rb.velocity.y);
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

    public void Die()
    {
        if (currentState == PlayerState.Dead) return;

        currentState = PlayerState.Dead;
        rb.velocity = Vector2.zero;

    #if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
    #endif

        Invoke(nameof(Restart), 0.4f);
    }

    void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

}
