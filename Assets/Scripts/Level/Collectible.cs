using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int id; // 0,1,2,3

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        GameManager.Instance.AddCollectible();
        CollectiblesUI.Instance.ActivateIcon(id);

        Destroy(gameObject);
    }
}
