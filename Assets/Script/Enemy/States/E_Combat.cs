using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Combat : E_Base
    {
        public E_Combat(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }
        public override void EnterState()
        {
            //Ready things for attack
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            // Select Attack Type
            // Maintain Distance for Attack If needed
            // Execute the attack
            base.LogicUpdateState();
        }
    }
}
