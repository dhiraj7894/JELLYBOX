using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{

    public class P_SpecialAttackCutScene : P_Base
    {
        public float TimeForCutScene = 0;
        public float TimeSpeed = 5;
        public float CurrentTime = 0;


        public float shieldSizeIncrese = 8;
        public float currentShieldSize;
        public P_SpecialAttackCutScene(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void EnterState()
        {
            base.EnterState();
            currentShieldSize = player.shieldParticle.GetFloat("Size");
            LeanTween.value(player.gameObject, currentShieldSize, shieldSizeIncrese, 0.2f).setOnUpdate((float val) => {player.shieldParticle.SetFloat("Size", val); });
            TimeForCutScene = player.stats.stats.SpecialAttack_A_EnergyLevel;
            CurrentTime = 0;
            player.anim.Play(AnimationVeriable.SPECIAL_A);
        }

        public override void ExitState()
        {
            base.ExitState();
            //player.shieldParticle.SetFloat("Size", currentShieldSize);
            LeanTween.value(player.gameObject, shieldSizeIncrese, currentShieldSize, 0.2f).setOnUpdate((float val) => { player.shieldParticle.SetFloat("Size", val); });
            player.anim.SetTrigger(AnimationVeriable.ENDSPA);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
            CurrentTime += TimeSpeed * Time.deltaTime;
            if(CurrentTime>= MainPlayer.GetPercentageOfValue(TimeForCutScene,60))
            {
                player.isSpecialAttackCooldown = false;
                player.ChangeCurrentState(player.IDLE);
            }
        }


        public static void ShieldSizeManipulator(float size)
        {

        }


    }
}