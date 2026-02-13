using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler
{
    public PlayerShooting shooting;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!shooting.player.CanShoot()) return;
        shooting.Shoot();
    }
}
