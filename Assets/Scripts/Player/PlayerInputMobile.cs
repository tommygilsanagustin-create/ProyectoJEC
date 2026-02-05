using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInputMobile : MonoBehaviour
{
    public PlayerController player;

    public void MoveLeft()
    {
        player.Move(-1);
    }

    public void MoveRight()
    {
        player.Move(1);
    }

    public void StopMove()
    {
        player.Move(0);
    }

    public void Jump()
    {
        player.Jump();
    }

    public void CrawlDown()
    {
        player.StartCrawl();
    }

    public void CrawlUp()
    {
        player.StopCrawl();
    }
}
