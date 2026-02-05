using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // matar jugador
        }
    }
}
