using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Petrol : E_Base
    {
        public E_Petrol(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();
            enemy.agent.SetDestination(enemy.PetrolPath[0].position);
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            //Set new Path for enemy to follow when it reaches it's current one

            //check for target
                // change state if near target
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}