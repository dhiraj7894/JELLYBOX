using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_Idle : E_Base
    {
        public E_Idle(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
                enemy.ChangeCurrentState(enemy.AttackList[0]);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                enemy.ChangeCurrentState(enemy.AttackList[1]);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                enemy.ChangeCurrentState(enemy.AttackList[2]);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                enemy.ChangeCurrentState(enemy.AttackList[3]);


            base.LogicUpdateState();
        }
    }
}
