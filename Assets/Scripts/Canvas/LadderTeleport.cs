using UnityEngine;

public class LadderTeleport : MonoBehaviour
{
    public Transform topPoint;
    public GameObject interactButton;

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            interactButton.SetActive(false);
        }
    }

    public void TeleportPlayer()
    {
        if (player != null)
        {
            player.transform.position = topPoint.position;
            interactButton.SetActive(false);
        }
    }
}
