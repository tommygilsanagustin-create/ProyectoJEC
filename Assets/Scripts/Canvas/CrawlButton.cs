using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrawlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerController player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.CanMove()) return;
        player.StartCrawl();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!player.CanMove()) return;
        player.StopCrawl();
    }

}
