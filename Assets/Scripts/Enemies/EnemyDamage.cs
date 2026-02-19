using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private EnemyAI enemyAI;

    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider
                .GetComponent<PlayerController>()
                .Die();

            enemyAI.ReturnToPointB();
        }
    }
}

