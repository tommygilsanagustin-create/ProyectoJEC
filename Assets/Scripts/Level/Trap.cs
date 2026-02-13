using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerController>().Die();
        }
    }
}
