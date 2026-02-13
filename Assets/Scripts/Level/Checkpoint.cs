using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (activated) return;

        if (col.CompareTag("Player"))
        {
            activated = true;
            GameManager.Instance.SetCheckpoint(transform.position);
        }
    }
}
