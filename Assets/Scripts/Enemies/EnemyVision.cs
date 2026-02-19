using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyAI enemyAI;

    void Awake()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyAI.StartChasing(other.transform);
        }
    }
}
