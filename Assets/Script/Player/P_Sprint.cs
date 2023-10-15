using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Player
{
    public class P_Sprint : P_Base
    {
        public P_Sprint(MainPlayer _player) : base(_player) {
            player = _player;
        }
        public override void EnterState()
        {
            base.EnterState();
            _isSprint = false;
            _input = Vector2.zero;
        }
        public override void ManageInput()
        {
            _input = _moveAction.ReadValue<Vector2>();

            if (_sprintAction.triggered || _input.sqrMagnitude == 0f)
            {
                _isSprint = false;
            }

        }
        public override void LogicUpdateState()
        {
            base.LogicUpdateState();            
            if (_input.magnitude >= 0.1f) MovementUpdate(player.sprintSpeedMultiplier);
            if (_isSprint)
            {
                player.animator.SetFloat(AnimationVeriable.SPEED, _input.magnitude + 0.5f, player.playerSpeedDamp, Time.deltaTime);                               

            }
            else
            {
                player.ChangeCurrentState(player.IDLE);
            }

        }
        public override void ExitState()
        {
        }

    }
}
