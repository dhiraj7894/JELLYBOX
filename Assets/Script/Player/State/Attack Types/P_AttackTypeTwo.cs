using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Player
{
    public class P_AttackTypeTwo : P_Base
    {
        public P_AttackTypeTwo(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void EnterState()
        {
            base.EnterState();
            player.ChangeCurrentState(player.IDLE);
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
        }

        public override void ManageInput()
        {
            base.ManageInput();
        }
    }
}
