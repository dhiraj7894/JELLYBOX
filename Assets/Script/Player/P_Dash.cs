using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class P_Dash : P_Base
    {
        public P_Dash(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void ManageInput() { 
        base.ManageInput();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
        }

        public override void ExitState() { 
        base.ExitState();
        }
    }
}
