using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public PlayerController player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    public void Shoot()
    {
        if (player == null)
        {
            Debug.LogError("Player NO asignado en PlayerShooting");
            return;
        }

        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab NO asignado");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("FirePoint NO asignado");
            return;
        }

        int dir = player.facingDirection;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Bullet b = bullet.GetComponent<Bullet>();
        if (b == null)
        {
            Debug.LogError("El prefab de bala NO tiene script Bullet");
            return;
        }

        b.Init(dir, bulletSpeed);
    }


}

