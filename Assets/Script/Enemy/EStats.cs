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
    [Serializable]
    public class Stamina
    {
        public float currentStamina;
        public float reducedStamina;
        public float StaminaDiductionSpeed;
        public float StaminaIncrementSpeed;
        public float speedMultiplier;
    }

    public class EStats : MonoBehaviour, IHealthSystem
    {
        public Health health;
        public Stamina stamina;
        public EnemyStatsSO stats;
        public MainEnemy enemy;

        public Slider healthSlider;
        public Slider staminaSlider;

        private void Start()
        {
            health.currentHealth = stats.MaxHealth;
            health.reducedHealth = health.currentHealth;
            
            stamina.currentStamina = stats.MaxStamina;
            stamina.reducedStamina = stamina.currentStamina;

            healthSlider.maxValue = health.currentHealth;
            healthSlider.value = health.currentHealth;

            staminaSlider.maxValue = stamina.currentStamina;
            staminaSlider.value = stamina.currentStamina;
        }

        private void Update()
        {
            HealthSlider();
            StaminaSlider();
        }


        public void TakeDamage(float damage)
        {
            if (stamina.currentStamina > 0)
            {
                health.reducedHealth -= (damage / (damage));
                stamina.reducedStamina -= (damage / (damage / 4));
            }
            else
            {
                health.reducedHealth -= damage;
            }
            
        }

        public void StaminaRefill()
        {
            stamina.reducedStamina = stats.MaxStamina;
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

        public void StaminaSlider()
        {
            if (stamina.reducedStamina > stamina.currentStamina)
            {
                if (stamina.reducedStamina >= stamina.StaminaIncrementSpeed)
                    stamina.currentStamina += stamina.StaminaIncrementSpeed * stamina.speedMultiplier * Time.deltaTime;
                else
                    stamina.currentStamina += stamina.StaminaIncrementSpeed * Time.deltaTime;

                if (stamina.reducedStamina <= stamina.currentStamina)
                {
                    stamina.currentStamina = stamina.reducedStamina;
                }
            }
            if (stamina.reducedStamina < stamina.currentStamina)
            {
                if (stamina.reducedStamina <= stamina.StaminaDiductionSpeed)
                    stamina.currentStamina -= stamina.StaminaDiductionSpeed * stamina.speedMultiplier * Time.deltaTime;
                else
                    stamina.currentStamina -= stamina.StaminaDiductionSpeed * Time.deltaTime;

                if (stamina.reducedStamina >= stamina.currentStamina)
                {
                    stamina.currentStamina = stamina.reducedStamina;
                }
            }
            staminaSlider.value = stamina.currentStamina;
        }
    }
}
