
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Player
{
    public class P_Attack : P_Base
    {
        float timePassed;
        float clipLength;
        float clipSpeed;
        bool attack;

        public P_Attack(MainPlayer _player) : base(_player)
        {
            player = _player;
        }
        public override void EnterState()
        {
            base.EnterState();
            //if (player.targetedEnemy) RotateTowardCamera();
            attack = false;
            timePassed = 0;

            if(player.currentStamina >= player.stats.stats.StaminaNeedToAttack)
            {
                player.anim.SetTrigger(AnimationVeriable.ATTACK);
                player.anim.SetFloat(AnimationVeriable.SPEED, 0);
                if (!player.isCooldown) player.doDash();
                player.currentStamina -= player.stats.stats.StaminaNeedToAttack;
            }
            else
            {
                player.ChangeCurrentState(player.IDLE);
                player.anim.SetTrigger(AnimationVeriable.MOVE);
            }

            
        }

        public override void ManageInput()
        {
            base.ManageInput();
            if (_attack.triggered)
            {
                attack = true;
            }            

        }
        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
            LightAttackLogic();
            //character.animator.SetBool(AnimationVeriable.BLOCK, _isBlocked);
        }

        public override void ExitState()
        {
            base.ExitState();
            player.anim.ResetTrigger(AnimationVeriable.ATTACK);
            player.isCooldown = false;
            // character.animator.applyRootMotion = false;            
        }

        public void LightAttackLogic()
        {
            timePassed += Time.deltaTime;
            clipLength = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = player.anim.GetCurrentAnimatorStateInfo(0).speed;
            
            if (!player.isCooldown && timePassed >= clipLength / clipSpeed && attack )
            {
                player.ChangeCurrentState(player.ATTACKING);
            }
            if (timePassed >= clipLength / clipSpeed)
            {
                player.ChangeCurrentState(player.IDLE);
                player.anim.SetTrigger(AnimationVeriable.MOVE);
            }
        }
    }
}

