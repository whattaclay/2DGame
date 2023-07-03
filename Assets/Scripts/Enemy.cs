﻿using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject deathEffect;
    public void TakeDamage(int damage)
    { 
        health -= damage;
        if (health<= 0 )
        {
            Die();
        }
    }
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}