using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;

    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;

    private Transform player;

    private bool chasingPlayer = false;
    private bool returningToB = false;

    float minX;
    float maxX;

    void Start()
    {
        minX = Mathf.Min(pointA.position.x, pointB.position.x);
        maxX = Mathf.Max(pointA.position.x, pointB.position.x);
    }

    void Update()
    {
        if (returningToB)
        {
            MoveToX(pointB.position.x);

            if (Mathf.Abs(transform.position.x - pointB.position.x) < 0.05f)
            {
                returningToB = false;
            }
            return;
        }

        if (chasingPlayer && player != null)
        {
            MoveToX(player.position.x);

            if (!IsPlayerInsideZone())
            {
                StopChasing();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float targetX = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;
        MoveToX(targetX);
    }

    void MoveToX(float targetX)
    {
        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector2(
            Mathf.MoveTowards(transform.position.x, clampedX, speed * Time.deltaTime),
            transform.position.y
        );
    }

    bool IsPlayerInsideZone()
    {
        return player.position.x >= minX && player.position.x <= maxX;
    }

    public void StartChasing(Transform playerTransform)
    {
        if (!IsInsideZone(playerTransform.position.x)) return;

        player = playerTransform;
        chasingPlayer = true;
    }

    void StopChasing()
    {
        chasingPlayer = false;
        player = null;
    }

    bool IsInsideZone(float x)
    {
        return x >= minX && x <= maxX;
    }

    // 🔥 ESTE ES EL MÉTODO IMPORTANTE
    public void ReturnToPointB()
    {
        chasingPlayer = false;
        player = null;
        returningToB = true;
    }
}
