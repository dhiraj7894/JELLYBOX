using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_HammerAttack : E_Base
    {
        public float currentJumpTime = 0;

        public float speed = 2f;
        public float time = 0f;
        public float height;

        public bool jumpExecuted = false;
        public bool isHammerHitted = false;

        GameObject hammerVisual = null;
        public E_HammerAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            //Initialize
            enemy.agent.enabled = false;
            jumpExecuted = false;
            isHammerHitted = false;
            base.EnterState();
        }

        public override void ExitState()
        {
            //Exit code
            enemy.agent.enabled = true;
            jumpExecuted = false;
            isHammerHitted = false;
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
                //Jump();
                //enemy.ShowAttackVisual(enemy.bossAttackData.hammerAttackVisual, enemy.transform.position, Quaternion.identity, enemy.bossAttackData.hammerAttackParant);
                hammerVisual =  enemy.WaveAttackVisual(enemy.bossAttackData.hammerAttackVisual, new Vector3(0,0,0), Quaternion.Euler(0,0,0), enemy.bossAttackData.hammerAttackParant);
                hammerVisual.transform.localRotation = Quaternion.Euler(0,0,0);
                jumpExecuted = true;
            }
            if (hammerVisual)
            {
                isHammerHitted = hammerVisual.GetComponent<HammerAttack>().isHammerAttackComplete;
            }

            if (isHammerHitted)
            {
                hammerVisual.transform.parent = null;
                enemy.ChangeCurrentState(enemy.IDLE);
            }
                


            /* try
             {
                 if (enemy.currentAttackVFX.GetComponent<HammerAttack>().isHammerAttackComplete && enemy.isGrounded)
                 {

                     enemy.ChangeCurrentState(enemy.IDLE);
                     enemy.currentAttackVFX.GetComponent<HammerAttack>().DestroyObject();
                 }
             }
             catch
             {

             }*/
            base.LogicUpdateState();
        }

        public void Jump()
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
                               
            }
        }
    }
}
