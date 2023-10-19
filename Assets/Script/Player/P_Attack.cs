
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Player
{
    [System.Serializable]public class P_Attack : P_Base
    {
        public float timePassed;
        float clipLength;
        float clipSpeed;
        bool attack;

        public float time;
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
            player.S_anim.SetTrigger(AnimationVeriable.ATTACK);
            player.P_anim.SetFloat(AnimationVeriable.SPEED, 0);
        }

        int i;

        public override void ManageInput()
        {
            base.ManageInput();
            if (_attack.triggered /*&& character.currentStamina >= character.GetComponent<WeaponImapct>().stats.StaminaNeed*/)
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
            player.S_anim.ResetTrigger(AnimationVeriable.ATTACK);
            player.isCooldown = false;
            // character.animator.applyRootMotion = false;
        }

        public void LightAttackLogic()
        {
            timePassed += Time.deltaTime;
            clipLength = player.S_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = player.S_anim.GetCurrentAnimatorStateInfo(0).speed * 1.2f;
            time = clipLength;
            if (!player.isCooldown && timePassed >= clipLength / clipSpeed && attack )
            {
                player.ChangeCurrentState(player.ATTACKING);
            }
            if (timePassed >= clipLength / clipSpeed)
            {
                player.ChangeCurrentState(player.IDLE);
                player.S_anim.SetTrigger(AnimationVeriable.MOVE);
            }
        }
    }
}

