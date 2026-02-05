using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerShooting shooting;
    public int direction; // -1 o 1

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!shooting.player.CanShoot()) return;
        shooting.Shoot(direction);
    }

}
