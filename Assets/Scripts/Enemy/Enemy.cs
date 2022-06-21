using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public int Damage(int damage)
    {
        if (damage <= 0)
        {
            return -1;
        }
        int overkillDamage = damage - currentHealth;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        return overkillDamage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
