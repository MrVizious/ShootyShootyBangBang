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

    public int maxNumberOfBullets = 5;
    [SerializeField]
    private List<int> bulletDamages = new List<int>();


    private void Start()
    {
        if (bulletDamages.Count <= 0)
        {
            for (int i = 0; i < maxNumberOfBullets; i++)
            {
                bulletDamages.Add(1);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // We spawn a new bullet
        PlayerBullet bullet = (PlayerBullet)bulletPool.Spawn();
        bullet.Shoot(transform.position, transform.up, bulletDamages[0]);
        bulletDamages.RemoveAt(0);
        bulletDamages.Add(1);
        shipMovement.Push(-transform.up * 20);
    }

    public void AddDamage(int damageToAdd)
    {
        for (int i = 0; i < bulletDamages.Count; i++)
        {
            bulletDamages[i] += damageToAdd;
        }
    }
}