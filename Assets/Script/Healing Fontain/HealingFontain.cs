using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;


namespace Jelly.Player
{
    public class HealingFontain : IActionTrigger
    {
        public float MaxHealing = 100;
        public float CooldownTime = 5;
        public bool isCooldown = false;
        public ParticleSystem HealingParticleForPlayer;

        public PressF_UI PressFUI;

        public void HealDamage() {
            if (!isCooldown)
            {
                if (PressFUI.stats.health.reducedHealth < PressFUI.stats.stats.MaxHealth)
                {
                 
                    //PressFUI.stats.health.reducedHealth = MaxHealing;
                    LeanTween.value(this.gameObject, PressFUI.stats.health.reducedHealth, MaxHealing, 1f).
                        setOnUpdate((float val) => { PressFUI.stats.health.reducedHealth = val; });
                    if (HealingParticleForPlayer) Destroy(Instantiate(HealingParticleForPlayer.gameObject, PressFUI.stats.transform), 1);
                    isCooldown = true;
                    MainPlayer.Instance.isInCutScene = true;
                    UIManager.Instance.CutSceneFadeOutIn(CooldownTime);
                    StartCoroutine(healingCooldown());

                }
            }
        }

        public override void Trigger()
        {
            HealDamage();
        }

        IEnumerator healingCooldown()
        {
            if (isCooldown)
            {
                yield return new WaitForSeconds(CooldownTime);
                isCooldown = false;
                MainPlayer.Instance.isInCutScene = false;
            }
            StopCoroutine(healingCooldown());
            
        }
    }
}