using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Enemy enemyData;

    private float currentMoveSpeed;
    private float currentHealth;
    private float currentDamage;

    private void Awake()
    {
        currentMoveSpeed = enemyData.speed;
        currentHealth = enemyData.health;
        currentDamage = enemyData.currentDamage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
