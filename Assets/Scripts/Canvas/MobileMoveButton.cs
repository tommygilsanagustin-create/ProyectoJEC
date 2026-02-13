using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float direction; // -1 izquierda, 1 derecha
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.Move(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.Move(0);
    }
}
