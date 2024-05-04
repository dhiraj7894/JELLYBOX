using Jelly.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Jelly.Enemy
{
    [Serializable]public class Health
    {
        public float currentHealth;
        public float reducedHealth;
        public float HealthDiductionSpeed;
        public float HealthIncrementSpeed;
        public float speedMultiplier;
    }
   
    public class EStats : MonoBehaviour, IHealthSystem
    {
        public Health health;
        public EnemyStatsSO stats;
        public MainEnemy enemy;

        public Slider healthSlider;


        private void Start()
        {
            health.currentHealth = stats.MaxHealth;
            health.reducedHealth = health.currentHealth;


            healthSlider.maxValue = health.currentHealth;
            healthSlider.value = health.currentHealth;
        }

        private void Update()
        {
            HealthSlider();
        }


        public void TakeDamage(float damage)
        {
            health.reducedHealth -= damage;
        }
        public void HealthSlider()
        {
            if (health.reducedHealth > health.currentHealth)
            {
                if (health.reducedHealth >= health.HealthIncrementSpeed)
                    health.currentHealth += health.HealthIncrementSpeed * health.speedMultiplier * Time.deltaTime;
                else
                    health.currentHealth += health.HealthIncrementSpeed * Time.deltaTime;

                if (health.reducedHealth <= health.currentHealth)
                {
                    health.currentHealth = health.reducedHealth;
                }
            }
            if (health.reducedHealth < health.currentHealth)
            {
                if (health.reducedHealth <= health.HealthDiductionSpeed)
                    health.currentHealth -= health.HealthDiductionSpeed * health.speedMultiplier * Time.deltaTime;
                else
                    health.currentHealth -= health.HealthDiductionSpeed * Time.deltaTime;

                if (health.reducedHealth >= health.currentHealth)
                {
                    health.currentHealth = health.reducedHealth;
                }
            }
            healthSlider.value = health.currentHealth;
        }
    }
}
