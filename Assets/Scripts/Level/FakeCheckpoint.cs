using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCheckpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().Die();
        }
    }
}
