using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Player
{
    public class P_SpecialAttack : P_Base
    {
        public P_SpecialAttack(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void EnterState()
        {
            base.EnterState();
        }
        public override void ManageInput()
        {
            base.ManageInput();
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
