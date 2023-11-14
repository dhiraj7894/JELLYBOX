using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Idle : E_Base
    {
        public E_Idle(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();

            enemy.SwitchPhysics(false);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            if (enemy.target)
            {
                if(CheckTargetDistance(enemy.target) < enemy.stats.AttackRadius)
                {
                    enemy.ChangeCurrentState(enemy.COMBAT);
                }
                else if(CheckTargetDistance(enemy.target) < enemy.stats.ChaseRadius)
                {
                    enemy.ChangeCurrentState(enemy.CHASE);
                }
            }
            else
            {
                enemy.ChangeCurrentState(enemy.PETROL);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}