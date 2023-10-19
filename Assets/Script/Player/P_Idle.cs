using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Player
{
    [System.Serializable]
    public class P_Idle : P_Base
    {
        public Vector3 velocity;
        public bool attack;
        public P_Idle(MainPlayer _player) : base(_player)
        {
            player = _player;
        }

        public override void EnterState()
        {
            base.EnterState();
            _input = Vector2.zero;
            _isSprint = false;
            attack = false;

        }


        public override void ManageInput()
        {
            base.ManageInput();
            velocity = _velocity;
            _input = _moveAction.ReadValue<Vector2>();
            if (_sprintAction.triggered)
            {
                _isSprint = true;
            }    
            if(_attack.triggered)
            {
                attack = true;
            }
        }


        public override void LogicUpdateState()
        {
            base.LogicUpdateState();

            player.P_anim.SetFloat(AnimationVeriable.SPEED, _input.magnitude, player.playerSpeedDamp, Time.deltaTime);

            if (_input.magnitude >= 0.1f) MovementUpdate();

            if (_isSprint && !attack)
            {                
                player.ChangeCurrentState(player.SPRINT);
            }
            if (attack && !_isSprint)
            {
                player.S_anim.SetTrigger(AnimationVeriable.ATTACK);
                player.P_anim.SetFloat(AnimationVeriable.SPEED, 0);
                player.ChangeCurrentState(player.ATTACKING);
            }
        }
        public override void ExitState()
        {
           
        }
    }
}
