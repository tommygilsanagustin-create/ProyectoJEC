using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class WASD : MonoBehaviour
{
    private PlayerController player;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrawl();
    }

    void HandleMovement()
    {
        float direction = 0f;

        if (Input.GetKey(KeyCode.A))
            direction = -1f;

        if (Input.GetKey(KeyCode.D))
            direction = 1f;

        player.Move(direction);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            player.Jump();
        }
    }

    void HandleCrawl()
    {
        if (Input.GetKey(KeyCode.S))
        {
            player.StartCrouch();
        }
        else
        {
            player.StopCrouch();
        }
    }
}