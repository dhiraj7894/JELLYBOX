using System;
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
            if(player.currentStamina >= (player.stats.stats.StaminaNeedToAttack* player.stats.stats.StaminaMultiplier) )
            {
                _heavyAttack.performed += OnHeavyAttack;
            }         
            
        }

        public float attackCoolDown = 2;
        public override void ManageInput()
        {
            base.ManageInput();
            velocity = _velocity;
            _input = _moveAction.ReadValue<Vector2>();  
            if(_attack.triggered && player.currentStamina >= player.stats.stats.StaminaNeedToAttack)
            {
                attack = true;
                attackCoolDown = 2;
            }

            if (attackCoolDown > 0) attackCoolDown -= Time.deltaTime;
            if (player.currentStamina < player.stats.stats.MaxStamina && !player.isStaminaCoolDown && attackCoolDown <= 0)
            {
                player.StartRefilStamina();
                player.isStaminaCoolDown = true;
            }

            if (_specialAttackA.triggered && !player.isSpecialAttackCooldown && player.isSpecialAttack_A_CanBePerforme)
            {
                player.isSpecialAttackCooldown = true;
                player.isSpecialAttack_A_CanBePerforme = false;
                SpecialAttackA();
            }
            if (_specialAttackB.triggered && !player.isSpecialAttackCooldown && player.isSpecialAttack_B_CanBePerforme)
            {
                player.isSpecialAttackCooldown = true;
                player.isSpecialAttack_B_CanBePerforme = false;
                SpecialAttackB();
            }
        }


        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
            
            if (!_isDead)
            {
                player.anim.SetFloat(AnimationVeriable.SPEED, _input.magnitude, player.playerSpeedDamp, Time.deltaTime);
                if (_input.magnitude >= 0.1f) MovementUpdate();

                if (!player.isSpecialAttackCooldown)
                {
                    if (_isSprint && !attack)
                    {
                        player.ChangeCurrentState(player.SPRINT);
                    }
                    if (attack && !_isSprint)
                    {
                        player.ChangeCurrentState(player.ATTACKING);
                    }
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
            player.ChangeCurrentState(player.HEAVYATTACK);
            player.currentStamina -= (player.stats.stats.StaminaNeedToAttack * player.stats.stats.StaminaMultiplier);
        }

        public void SpecialAttackA()
        {
            player.SPA();
            player.ChangeCurrentState(player.SPECIALATTACK);
        }
        public void SpecialAttackB()
        {
            player.SPB();
            player.ChangeCurrentState(player.SPECIALATTACK);            
        }

    }
}
