using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject interactButton;

    private bool playerInside;

    void Start()
    {
        interactButton.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInside = true;
            interactButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerInside = false;
            interactButton.SetActive(false);
        }
    }

    public void PickUpKey()
    {
        if (!playerInside) return;

        GameManager.Instance.PickKey();
        interactButton.SetActive(false);
        Destroy(gameObject);
    }
}
