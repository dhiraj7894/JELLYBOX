using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Pursuit : E_Base
    {
        public E_Pursuit(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            //Enable somthing which is needed
            base.EnterState();
        }

        public override void ExitState()
        {
            //Reset
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            // Follow Player
            // Attack if player is in radius
            // Rotate if it in rotate radius = Add smothness in rotation so that is look like it is roatating slowly
            // 
            base.LogicUpdateState();
        }



    }
}