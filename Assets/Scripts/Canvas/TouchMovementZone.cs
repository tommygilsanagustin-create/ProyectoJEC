using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMovementZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public PlayerController player;

    private int direction = 0;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.CanMove()) return;
        UpdateDirection(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!player.CanMove()) return;
        UpdateDirection(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        direction = 0;
        player.Move(0);
    }

    void UpdateDirection(PointerEventData eventData)
    {
        float screenMid = Screen.width / 2f;
        direction = eventData.position.x < screenMid ? -1 : 1;
        player.Move(direction);
    }
}
