using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Attack : E_Base
    {
        public E_Attack(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}