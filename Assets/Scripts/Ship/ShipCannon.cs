using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DebugExtension;

public class ShipCannon : MonoBehaviour
{
    public Pool bulletPool;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PlayerBullet bullet = (PlayerBullet)bulletPool.Spawn();
            bullet.Shoot(transform.position, transform.up);
        }
    }
}