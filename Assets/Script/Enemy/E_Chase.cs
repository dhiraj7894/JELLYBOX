using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Chase : E_Base
    {
        public E_Chase(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.SwitchPhysics(false);
            enemy.agent.SetDestination(enemy.target.position);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            if (CheckTargetDistance(enemy.target) < enemy.stats.ChaseRadius)
            {
                enemy.agent.SetDestination(enemy.target.position);
            }
            else if (CheckTargetDistance(enemy.target) < enemy.stats.AttackRadius)
            {
                enemy.ChangeCurrentState(enemy.COMBAT);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}