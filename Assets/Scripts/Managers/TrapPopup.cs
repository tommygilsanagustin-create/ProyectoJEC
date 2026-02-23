using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPopup : MonoBehaviour
{
    public Transform trap;

    public float hiddenY;
    public float shownY;
    public float moveSpeed = 4f;

    private bool playerInside = false;

    void Update()
    {
        float targetY = playerInside ? shownY : hiddenY;

        trap.position = new Vector2(
            trap.position.x,
            Mathf.MoveTowards(trap.position.y, targetY, moveSpeed * Time.deltaTime)
        );
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInside = false;
    }
}
