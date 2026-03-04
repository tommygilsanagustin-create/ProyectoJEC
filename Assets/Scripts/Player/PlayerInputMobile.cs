using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputMobile : MonoBehaviour
{
    public PlayerController player;

    private float moveDirection = 0f;

    void Update()
    {
        if (moveDirection != 0)
        {
            player.Move(moveDirection);
        }
        else
        {
            player.Move(0);
        }
    }

    // BOTÓN IZQUIERDA
    public void OnLeftDown()
    {
        moveDirection = -1f;
    }

    public void OnLeftUp()
    {
        if (moveDirection < 0)
            moveDirection = 0f;
    }

    // BOTÓN DERECHA
    public void OnRightDown()
    {
        moveDirection = 1f;
    }

    public void OnRightUp()
    {
        if (moveDirection > 0)
            moveDirection = 0f;
    }

    public void Jump()
    {
        player.Jump();
    }

    public void CrouchDown()
    {
        player.StartCrouch();
    }

    public void CrouchUp()
    {
        player.StopCrouch();
    }
}