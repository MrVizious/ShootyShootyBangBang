using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomEvents;
using DebugExtension;

public class ShipCannon : MonoBehaviour
{
    public Pool bulletPool;

    public ShipMovement shipMovement;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PlayerBullet bullet = (PlayerBullet)bulletPool.Spawn();
            bullet.Shoot(transform.position, transform.up);
            shipMovement.Push(-transform.up * 20);
        }
    }
}