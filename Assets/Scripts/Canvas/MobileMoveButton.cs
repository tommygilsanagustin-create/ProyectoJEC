using UnityEngine;
using UnityEngine.EventSystems;

public class MobileMoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float direction; // -1 izquierda | 1 derecha
    private PlayerController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
            player.SetMoveInput(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
            player.SetMoveInput(0);
    }
}