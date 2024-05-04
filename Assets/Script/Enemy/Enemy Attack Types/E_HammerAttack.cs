using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    [System.Serializable]
    public class E_HammerAttack : E_Base
    {
        public float currentJumpTime = 0;

        public float speed = 2f;
        public float time = 0f;
        public float height;

        public bool jumpExecuted = false;
        public E_HammerAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            //Initialize
            enemy.agent.enabled = false;
            jumpExecuted = false;
            base.EnterState();
        }

        public override void ExitState()
        {
            //Exit code
            enemy.agent.enabled = true;
            jumpExecuted = false;
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            //Cooldown wait
            //Summon Hammer
            //Impact Area
            //Attack
            //Reset
            if (currentJumpTime >= 0 && enemy.isGrounded) currentJumpTime -= Time.deltaTime;

            if (currentJumpTime <= 0 && !jumpExecuted)
            {
                JumpWave();

            }

            if (enemy.currentAttackVFX != null)
            {
                try
                {
                    if (enemy.currentAttackVFX.GetComponent<HammerAttack>().isHammerAttackComplete && enemy.isGrounded)
                    {

                        enemy.ChangeCurrentState(enemy.IDLE);
                        enemy.currentAttackVFX.GetComponent<HammerAttack>().DestroyObject();
                    }
                }
                catch
                {

                }
            }
            base.LogicUpdateState();
        }

        public void JumpWave()
        {
            // Increment time based on the elapsed time since the last frame
            time += Time.deltaTime * speed;

            // Evaluate the animation curve at the current time to get the height
            speed = enemy.bossAttackData.speedWaveCurve.Evaluate(time);
            height = enemy.bossAttackData.hammerWaveCurve.Evaluate(time);


            // Calculate the position of the cube based on the height from the curve
            Vector3 newPosition = enemy.transform.position;
            newPosition.y = height;

            // Move the cube to the new position
            enemy.transform.position = newPosition;

            // Reset time to 0 when the animation finishes (to loop the animation)
            if (time >= enemy.bossAttackData.hammerWaveCurve.keys[enemy.bossAttackData.hammerWaveCurve.length - 1].time)
            {
                time = 0f;
                jumpExecuted = true;
                //Spwan Attack Visuals

            }

            if (time >= .1f)
            {
                enemy.ShowAttackVisual(enemy.bossAttackData.hammerAttackVisual, enemy.transform.position, Quaternion.identity, false, true);                
            }
        }
    }
}
