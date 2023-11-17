using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Combat : E_Base
    {
        public E_Combat(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();

            enemy.SwitchPhysics(true);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            if (CheckTargetDistance(enemy.target) < enemy.stats.ChaseRadius)
            {
                enemy.ChangeCurrentState(enemy.CHASE);
            }
            
            
            if (CheckTargetDistance(enemy.target) < enemy.stats.AttackRadius)
            {
                enemy.ChangeCurrentState(enemy.ATTACK);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}