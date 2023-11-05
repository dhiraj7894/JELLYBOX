using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [Serializable]
    public struct UltCharge
    {
        public float currentCharge;
        public float reduceCharge;
        public float chargeDiductionSpeed;
        public float chargeIncrementSpeed;
        public float speedMultiplier;
    }

    public class PlayerStats : MonoBehaviour, IDamagable
    {
        
        public PlayerHealth health; 
        public PlayerStamina stamina; 
        public UltCharge charge;
                
        public Stats stats;
        public MainPlayer player;
        public float SpecialACharge = 10;
        private void Awake()
        {
            
        }
        public float vfx_Shield_Lifetime = 0;
        public float vfx_Shield_TimeVariation = 0.1f;
        private void Start()
        {
            health.currentHealth = stats.MaxHealth;
            health.reducedHealth = health.currentHealth;
            stamina.reducedStamina = player.currentStamina;

            charge.currentCharge = stats.SpecialAttack_B_EnergyLevel;
            charge.reduceCharge = charge.currentCharge;


            UIManager.Instance.Health.maxValue = health.currentHealth;
            UIManager.Instance.Health.value = health.currentHealth;

            UIManager.Instance.Stamina.maxValue = stats.MaxStamina;
            UIManager.Instance.Stamina.value = player.currentStamina;

            UIManager.Instance.SP_A.maxValue = stats.SpecialAttack_A_EnergyLevel;
            UIManager.Instance.SP_A.value = stats.SpecialAttack_A_EnergyLevel;


            UIManager.Instance.SP_B.maxValue = charge.currentCharge;
            UIManager.Instance.SP_B.value = charge.currentCharge;



            SpecialACharge = stats.SpecialAttack_A_EnergyLevel;

            vfx_Shield_Lifetime = player.shieldParticle.GetFloat("Lifetime");

            player.isSpecialAttack_A_CanBePerforme = true;
            player.isSpecialAttack_B_CanBePerforme = true;
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
        public IEnumerator ShieldReset()
        {
            UIManager.Instance.Shield.value = 0;
            if (player.isShieldActivated)
            {
                Debug.Log("ShieldActivated");
                yield return new WaitForSeconds(vfx_Shield_Lifetime);
                while (UIManager.Instance.Shield.value < vfx_Shield_Lifetime)
                {
                    Debug.Log("ShieldActivated Refieling");
                    UIManager.Instance.Shield.value += vfx_Shield_TimeVariation * Time.deltaTime;
                    yield return null;
                }
                // Start UI refiel time for new shield
                /*yield return new WaitForSeconds(vfx_Shield_TimeVariation);*/
                player.isShieldActivated = false;
            }
        }
        public IEnumerator RefielSpecialA()
        {
            SpecialACharge = 0;
            UIManager.Instance.SP_A.value = SpecialACharge;
            while(SpecialACharge < UIManager.Instance.SP_A.maxValue)
            {
                SpecialACharge += stats.SpecialAttackARechargeSpeed * Time.deltaTime;
                UIManager.Instance.SP_A.value = SpecialACharge;
                if (SpecialACharge >= UIManager.Instance.SP_A.maxValue)
                {
                    SpecialACharge = UIManager.Instance.SP_A.maxValue;                    
                }
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("While is looping");
            yield return new WaitForSeconds(stats.SpecialAttackACooldownTime);
            player.isSpecialAttack_A_CanBePerforme = true;
            StopCoroutine(RefielSpecialA());
        }
        public void RefielSpecialB()
        {
            charge.reduceCharge = 0;
        }
        private void Update()
        {
            HealthSlider();
            StaminaSlider();
            UltChargeRefiel();

            HealthManipulator();
        }

        /// <summary>
        /// For testing only
        /// </summary>
        public void HealthManipulator()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //health.reducedHealth += 10;
                charge.reduceCharge += 1;
                if(charge.currentCharge >= stats.SpecialAttack_B_EnergyLevel - 1)
                {
                    player.isSpecialAttack_B_CanBePerforme = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //health.reducedHealth -= 10;
                if(charge.reduceCharge>0) charge.reduceCharge -= 10;
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
        public void GetShieldVFXLifetime()
        {
            vfx_Shield_Lifetime = player.shieldParticle.GetFloat("Lifetime");
            UIManager.Instance.Shield.maxValue = vfx_Shield_Lifetime;
            UIManager.Instance.Shield.value = vfx_Shield_Lifetime;
        }
        public void UltChargeRefiel()
        {
            if (charge.reduceCharge > charge.currentCharge)
            {
                if (charge.reduceCharge >= charge.chargeIncrementSpeed)
                    charge.currentCharge += charge.chargeIncrementSpeed * charge.speedMultiplier * Time.deltaTime;
                else
                    charge.currentCharge += charge.chargeIncrementSpeed * Time.deltaTime;

                if (charge.reduceCharge <= charge.currentCharge)
                {
                    charge.currentCharge = charge.reduceCharge;
                }
            }
            if (charge.reduceCharge < charge.currentCharge)
            {
                if (charge.reduceCharge <= charge.chargeDiductionSpeed)
                    charge.currentCharge -= charge.chargeDiductionSpeed * charge.speedMultiplier * Time.deltaTime;
                else
                    charge.currentCharge -= charge.chargeDiductionSpeed * Time.deltaTime;

                if (charge.reduceCharge >= charge.currentCharge)
                {
                    charge.currentCharge = charge.reduceCharge;
                }
            }
            UIManager.Instance.SP_B.value = charge.currentCharge;
        }
    }

}

