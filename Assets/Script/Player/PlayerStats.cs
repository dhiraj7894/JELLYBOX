using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Player
{
    [Serializable] public struct PlayerHealth
    {
        public float currentHealth;
        public float reducedHealth;
        public float HealthDiductionSpeed;
        public float HealthIncrementSpeed;
        public float speedMultiplier;
    }

    [Serializable]
    public struct PlayerStamina
    {
        public float reducedStamina;
        public float StaminaDiductionSpeed;
    }



    public class PlayerStats : MonoBehaviour, IDamagable
    {
        
        public PlayerHealth health; 
        public PlayerStamina stamina; 
                
        public Stats stats;
        public MainPlayer player;
    

        private void Start()
        {
            health.currentHealth = stats.MaxHealth;
            health.reducedHealth = health.currentHealth;
            stamina.reducedStamina = player.currentStamina;
            UIManager.Instance.Health.maxValue = health.currentHealth;
            UIManager.Instance.Health.value = health.currentHealth;

            UIManager.Instance.Stamina.maxValue = stats.MaxStamina;
            UIManager.Instance.Stamina.value = player.currentStamina;


        }

        public void TakeDamage(float damage) { 
            health.currentHealth -= damage;
        }

        public IEnumerator StaminaRefil()
        {
            
            Debug.Log("Refil Activated");
            yield return new WaitForSeconds(stats.StaminaCoolDownTime);
            player.isUsableStaminaRestored = false;
            while (player.isStaminaCoolDown && player.currentStamina < stats.MaxStamina)
            {
                player.currentStamina += stats.StaminaRefilSpeed * Time.deltaTime;
                if(player.currentStamina >= stats.MaxStamina)
                {
                    player.currentStamina = stats.MaxStamina;
                    player.isStaminaCoolDown = false;
                    Debug.Log("Refil Complete");
                }
                yield return null;
            }
        }


        private void Update()
        {
            HealthSlider();
            StaminaSlider();

            HealthManipulator();
        }

        /// <summary>
        /// For testing only
        /// </summary>
        public void HealthManipulator()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                health.reducedHealth += 10;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                health.reducedHealth -= 10;
            }
        }

        public void HealthSlider()
        {
            if(health.reducedHealth > health.currentHealth)
            {
                if(health.reducedHealth >= health.HealthIncrementSpeed)
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
            UIManager.Instance.Health.value = health.currentHealth;
        }

        public void StaminaSlider()
        {
            if (stamina.reducedStamina > player.currentStamina)
            {
                stamina.reducedStamina -= stamina.StaminaDiductionSpeed * Time.deltaTime;
                if (stamina.reducedStamina <= player.currentStamina)
                {
                    stamina.reducedStamina = player.currentStamina;
                }
            }
            if (stamina.reducedStamina < player.currentStamina)
            {
                stamina.reducedStamina += stats.StaminaRefilSpeed * Time.deltaTime;

                if (stamina.reducedStamina >= player.currentStamina)
                {
                    stamina.reducedStamina = player.currentStamina;
                }
            }
            UIManager.Instance.Stamina.value = stamina.reducedStamina;
        }
    }
}

