using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.CanJump()) return;
        player.Jump();
    }

}
