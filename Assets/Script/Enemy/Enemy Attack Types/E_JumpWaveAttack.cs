using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_JumpWaveAttack : E_Base
    {
        public float currentJumpTime = 0;
        public float currentJumpCount = 0;


        public float speed = 2f;
        public float time = 0f;
        public float height;

        bool isJumping = false;
        public E_JumpWaveAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            enemy.agent.enabled = false;
            currentJumpCount = 0;
            currentJumpTime = enemy.maxJumpTime;
            base.EnterState();
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
            base.ExitState();
        }


        public override void LogicUpdateState()
        {
            if (currentJumpCount >= enemy.maxJumpCount)
                enemy.ChangeCurrentState(enemy.IDLE);
            if (currentJumpTime > 0)
            {
                currentJumpTime -= Time.deltaTime;
                if (currentJumpTime <= 1)
                {
                    JumpWave();
                    isJumping = true;
                }
            }
        }

        public void JumpWave()
        {
            // Increment time based on the elapsed time since the last frame
            time += Time.deltaTime * speed;

            // Evaluate the animation curve at the current time to get the height
            height = enemy.jumpWaveCurve.Evaluate(time);

            // Calculate the position of the cube based on the height from the curve
            Vector3 newPosition = enemy.transform.position;
            newPosition.y = height;

            // Move the cube to the new position
            enemy.transform.position = newPosition;

            // Reset time to 0 when the animation finishes (to loop the animation)
            if (time >= enemy.jumpWaveCurve.keys[enemy.jumpWaveCurve.length - 1].time)
            {
                time = 0f;
                enemy.ShowAttackVisual(enemy.jumpForceVisual, enemy.transform.position, Quaternion.identity);
                currentJumpTime = enemy.maxJumpTime;
                currentJumpCount++;
                Debug.Log($"{currentJumpCount} && {enemy.maxJumpCount}");
            }
        }

    }
}
