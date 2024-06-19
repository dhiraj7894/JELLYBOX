using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jelly.Enemy
{

    public class E_CircleAttack : E_Base
    {
        public float currentJumpTime = 0;

        public float speed = 2f;
        public float time = 0f;
        public float height;

        public bool jumpExecuted = false;
        GameObject visual;
        public E_CircleAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            enemy.agent.enabled = false;
            currentJumpTime = 1;
            base.EnterState();
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
            jumpExecuted = false;
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            if (currentJumpTime >= 0 && enemy.isGrounded) currentJumpTime -= Time.deltaTime;

            if (currentJumpTime <= 0 && !jumpExecuted)
            {
                JumpWave();
                
            }
            if(visual != null)
            {
                try
                {
                    if(visual.GetComponent<CircleAttack>().isCircleAttackComplete) {
                        
                        enemy.ChangeCurrentState(enemy.IDLE);
                        visual.GetComponent<CircleAttack>().DestroyObject();
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
            height = enemy.bossAttackData.circleWaveCurve.Evaluate(time);


            // Calculate the position of the cube based on the height from the curve
            Vector3 newPosition = enemy.transform.position;
            newPosition.y = height;

            // Move the cube to the new position
            enemy.transform.position = newPosition;

            // Reset time to 0 when the animation finishes (to loop the animation)
            if (time >= enemy.bossAttackData.circleWaveCurve.keys[enemy.bossAttackData.circleWaveCurve.length - 1].time)
            {
                time = 0f;
                //Spwan Attack Visuals
                visual = enemy.WaveAttackVisual(enemy.bossAttackData.circleAttackVisual, enemy.bossAttackData.circleparant.position, Quaternion.identity, enemy.bossAttackData.circleparant);
                jumpExecuted = true;
            }
        }
    }
}
