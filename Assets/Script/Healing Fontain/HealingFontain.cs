using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Game.Player
{
    public class HealingFontain : MonoBehaviour, IDamagable
    {
        public float MaxHealing = 100;
        public float CooldownTime = 5;
        public bool isCooldown = false;
        public PlayerStats playerStats;
        public ParticleSystem HealingParticleForPlayer;

        public GameObject InteractUI;

        Collider otherCollider;


        public void TakeDamage(float damage) { }
        public void HealDamage(float damage) {
            if (!isCooldown)
            {
                if (playerStats.health.reducedHealth < playerStats.stats.MaxHealth)
                {
                    playerStats.health.reducedHealth = MaxHealing;
                    if (HealingParticleForPlayer) Destroy(Instantiate(HealingParticleForPlayer.gameObject, playerStats.transform), 1);
                    isCooldown = true;
                    StartCoroutine(healingCooldown());

                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.tag);
            if (!isCooldown && other.CompareTag(TagHash.PLAYER))
            {
                if (other.GetComponent<PlayerStats>().health.reducedHealth < other.GetComponent<PlayerStats>().stats.MaxHealth)
                {
                    other.GetComponent<PlayerStats>().health.reducedHealth = MaxHealing;
                    if (HealingParticleForPlayer) Destroy(Instantiate(HealingParticleForPlayer.gameObject, other.transform), 1);
                    isCooldown = true;
                    StartCoroutine(healingCooldown());

                }
            }
        }

        public void showInteractUI()
        {
            InteractUI.GetComponent<CanvasGroup>().alpha = 1;
            //LeanTween.value(InteractUI.GetComponent<CanvasGroup>().alpha, 1, 0.1f);
        }
        public void hideInteractUI()
        {
            InteractUI.GetComponent<CanvasGroup>().alpha = 0;
            //LeanTween.value(InteractUI.GetComponent<CanvasGroup>().alpha, 0, 0.1f);
        }

        IEnumerator healingCooldown()
        {
            if (isCooldown)
            {
                yield return new WaitForSeconds(CooldownTime);
                isCooldown = false;
            }
            StopCoroutine(healingCooldown());
            
        }
    }
}