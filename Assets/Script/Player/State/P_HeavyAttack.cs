using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class P_HeavyAttack : P_Base
    {
        float timePassed;
        float clipLength;
        float clipSpeed;
        bool attack;

        float dashMultiplier;
        public P_HeavyAttack(MainPlayer _player) : base(_player)
        {
            player = _player;
        }

        public override void EnterState()
        {
            base.EnterState();
            dashMultiplier = 2;
            timePassed = 0;
            player.anim.SetTrigger(AnimationVeriable.HEAVYATTACK);
            player.anim.SetFloat(AnimationVeriable.SPEED, 0);
            player.doDash(dashMultiplier);
        }
        public override void ManageInput()
        {
            base.ManageInput();
        }

        public override void LogicUpdateState()
        {
            base.LogicUpdateState();
            HeavyAttackLogic();
        }

        public override void ExitState()
        {
            base.ExitState();
            player.anim.ResetTrigger(AnimationVeriable.HEAVYATTACK);
            if(player.isCooldown) player.isCooldown = false;
        }
        public void HeavyAttackLogic()
        {
            timePassed += Time.deltaTime;
            clipLength = player.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            clipSpeed = player.anim.GetCurrentAnimatorStateInfo(0).speed * 1.2f;            
            if (timePassed >= clipLength / clipSpeed)
            {
                player.ChangeCurrentState(player.IDLE);
                player.anim.SetTrigger(AnimationVeriable.MOVE);
            }
        }
    }
}