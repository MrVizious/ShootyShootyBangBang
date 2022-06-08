using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCannon : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PlayerBullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity)
                                  .GetComponent<PlayerBullet>();
            bullet.Shoot(transform.up);
        }
    }
}