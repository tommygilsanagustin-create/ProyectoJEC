using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 8f;

    private Vector2 startPos;
    private Rigidbody2D rb;

    public void Init(int direction, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // ❌ nada de caer
        rb.velocity = new Vector2(direction * speed, 0f);

        startPos = transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
