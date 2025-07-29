using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    private float health;
    public float startHealth = 100;
    public int worth = 1;
    [HideInInspector]
    public float speed;
    public float startSpeed = 10f;

    public GameObject deathEffectPrefab;
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
        healthBar.fillAmount = health / startHealth;

        return;
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1 - pct);
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject);
        PlayerStats.Money += worth;
        WaveSpawner.EnemiesAlive--;
        GameObject deathEffect = (GameObject)Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(deathEffect, 2f);
    }
    

}
