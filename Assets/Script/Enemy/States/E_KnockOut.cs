using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_KnockOut : E_Base
    {
        public E_KnockOut(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            enemy.GFXHEightManager(0, .7f); 
            enemy.durationOfKnockTime = enemy.maxKnockOutDuration;
            enemy.anim.Play("KnockOut");
        }

        public override void ExitState()
        {
            enemy.anim.SetTrigger("knockOver");
            base.ExitState();
        }

        public override void LateLogicUpdateState()
        {
            if(enemy.durationOfKnockTime > 0)
            {
                enemy.durationOfKnockTime -=Time.deltaTime;
                if(enemy.durationOfKnockTime <= 0)
                {
                    enemy.ChangeCurrentState(enemy.IDLE);
                    enemy.stats.StaminaRefill();
                }
            }
            base.LateLogicUpdateState();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
        }
    }
}
