using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
            _heavyAttack.performed += OnHeavyAttack;

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
            
            if (!_isDead)
            {
                player.anim.SetFloat(AnimationVeriable.SPEED, _input.magnitude, player.playerSpeedDamp, Time.deltaTime);
                if (_input.magnitude >= 0.1f) MovementUpdate();

                if (_isSprint && !attack)
                {
                    player.ChangeCurrentState(player.SPRINT);
                }
                if (attack && !_isSprint)
                {
                    /*                player.anim.SetTrigger(AnimationVeriable.ATTACK);
                                    player.anim.SetFloat(AnimationVeriable.SPEED, 0);*/
                    player.ChangeCurrentState(player.ATTACKING);
                }
            }
            else
            {
                player.anim.SetFloat(AnimationVeriable.SPEED, 0, player.playerSpeedDamp, Time.deltaTime);
            }
            
        }
        public override void ExitState()
        {
            _heavyAttack.performed -= OnHeavyAttack;
        }
        public void OnHeavyAttack(InputAction.CallbackContext context)
        {
/*            player.anim.SetTrigger(AnimationVeriable.HEAVYATTACK);*/
            player.ChangeCurrentState(player.HEAVYATTACK);
        }

    }
}
