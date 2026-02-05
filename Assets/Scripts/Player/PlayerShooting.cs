using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public PlayerController player;


    public void Shoot(int direction)
    {
        if (player.currentState == PlayerState.Crawl) return;
        if (player.currentState == PlayerState.Dead) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * 10f, 0);
    }

}
