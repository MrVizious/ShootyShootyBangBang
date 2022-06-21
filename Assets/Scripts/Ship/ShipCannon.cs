using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CustomEvents;
using DebugExtension;
using Sirenix.OdinInspector;

public class ShipCannon : SerializedMonoBehaviour
{
    public Pool bulletPool;

    public ShipMovement shipMovement;


    private int _currentNumberOfBullets = 0;

    [ShowInInspector]
    public int currentNumberOfBullets
    {
        get
        {
            return _currentNumberOfBullets;
        }
        set
        {
            value = Mathf.Clamp(value, 0, maxNumberOfBullets);
            int bulletsToAdd = value - currentNumberOfBullets;
            _currentNumberOfBullets = value;
            LoadBullet(bulletsToAdd);
        }
    }
    public int initialNumberOfBullets = 5;
    public int maxNumberOfBullets = 20;
    public int initialBulletDamage = 1;
    public int maxBulletDamage = 1000000;

    [SerializeField]
    private List<int> currentBullets = new List<int>();


    private void Start()
    {
        AddBullet(initialNumberOfBullets);
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
        bullet.Shoot(transform.position, transform.up, currentBullets[0]);
        currentBullets.RemoveAt(0);
        LoadBullet();
        shipMovement.Push(-transform.up * 20);
    }

    public void AddDamage(int damageToAdd)
    {
        for (int i = 0; i < currentBullets.Count; i++)
        {
            currentBullets[i] += damageToAdd;
            currentBullets[i] = Mathf.Clamp(currentBullets[i], initialBulletDamage, maxBulletDamage);
        }
    }

    public void SetDamage(int damage)
    {
        damage = Mathf.Clamp(damage, initialBulletDamage, maxBulletDamage);
        for (int i = 0; i < currentBullets.Count; i++)
        {
            currentBullets[i] = damage;
        }
    }

    public void LoadBullet(int bulletsToAdd = 1)
    {
        if (bulletsToAdd > 0)
        {
            for (int i = 0; i < bulletsToAdd; i++)
            {
                if (currentBullets.Count < currentNumberOfBullets)
                {
                    currentBullets.Add(initialBulletDamage);
                }
            }
        }
        else if (bulletsToAdd < 0)
        {
            for (int i = 0; i < -bulletsToAdd; i++)
            {
                if (currentBullets.Count > 0)
                {
                    currentBullets.RemoveAt(currentBullets.Count - 1);
                }
            }
        }
    }

    [Button("Add Bullet")]
    public void AddBullet(int bulletsToAdd = 1)
    {
        Debug.Log("Adding " + bulletsToAdd + " bullets");
        if (bulletsToAdd > 0)
        {
            for (int i = 0; i < bulletsToAdd; i++)
            {
                currentNumberOfBullets++;
            }
        }
        else if (bulletsToAdd < 0)
        {
            RemoveBullet(-bulletsToAdd);
        }
    }

    [Button("Remove Bullet")]
    public void RemoveBullet(int bulletsToRemove = 1)
    {
        if (bulletsToRemove > 0)
        {
            for (int i = 0; i < bulletsToRemove; i++)
            {
                currentNumberOfBullets--;
            }
        }
        else if (bulletsToRemove < 0)
        {
            AddBullet(-bulletsToRemove);
        }
    }

}