using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFloor : MonoBehaviour
{
    public float disappearDelay = 0.1f;
    public bool respawn = false;
    public float respawnTime = 2f;

    private Collider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke(nameof(Disappear), disappearDelay);
        }
    }

    void Disappear()
    {
        col.enabled = false;
        sr.enabled = false;

        if (respawn)
            Invoke(nameof(Reappear), respawnTime);
    }

    void Reappear()
    {
        col.enabled = true;
        sr.enabled = true;
    }
}
