using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
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