using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Preparation : E_Base
    {
        public E_Preparation(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }
        public override void EnterState()
        {
            //Start Preparation Animation
            base.EnterState();
        }

        public override void ExitState()
        {
            //Complete Animation
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            //Continue Animation
            base.LogicUpdateState();
        }
    }
}
