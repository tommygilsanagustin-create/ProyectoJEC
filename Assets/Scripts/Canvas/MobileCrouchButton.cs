using UnityEngine;
using UnityEngine.EventSystems;

public class MobileCrouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private PlayerController player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        player.StartCrouch();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.StopCrouch();
    }
}

