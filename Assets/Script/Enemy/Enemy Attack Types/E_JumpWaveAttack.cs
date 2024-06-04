using Ink.Parsed;
using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Jelly.Enemy
{
    [System.Serializable]
    public class E_JumpWaveAttack : E_Base
    {
        public float currentJumpTime = 0;
        public float currentJumpCount = 0;


        public float speed = 2f;
        public float time = 0f;
        public float height;

        bool isJumping = false;
        bool isWaveOver = false;
        GameObject WaveGameObject = null;
        public E_JumpWaveAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            isJumping = false;
            isWaveOver = false;
            WaveGameObject = null;
            enemy.agent.enabled = false;
            currentJumpCount = 0;
            currentJumpTime = enemy.bossAttackData.maxJumpTime;
            base.EnterState();
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
            isJumping = false;
            isWaveOver = false;

            base.ExitState();
        }


        public override void LogicUpdateState()
        {
            if (isWaveOver)
                enemy.ChangeCurrentState(enemy.IDLE);

            if (currentJumpTime > 0)
            {
                currentJumpTime -= Time.deltaTime;
                if (currentJumpTime <= 1 && !isJumping)
                {
                    //JumpWave();
                    Wave();
                    isJumping = true;
                }
            }

            if (WaveGameObject)
            {
                isWaveOver = WaveGameObject.GetComponent<WaveAttacks>().isWaveOver;
            }
        }

        public void JumpWave()
        {
            // Increment time based on the elapsed time since the last frame
            time += Time.deltaTime * speed;

            // Evaluate the animation curve at the current time to get the height
            height = enemy.bossAttackData.jumpWaveCurve.Evaluate(time);

            // Calculate the position of the cube based on the height from the curve
            Vector3 newPosition = enemy.transform.position;
            newPosition.y = height;

            // Move the cube to the new position
            enemy.transform.position = newPosition;

            // Reset time to 0 when the animation finishes (to loop the animation)
            if (time >= enemy.bossAttackData.jumpWaveCurve.keys[enemy.bossAttackData.jumpWaveCurve.length - 1].time)
            {
                time = 0f;
                //enemy.ShowAttackVisual(enemy.bossAttackData.jumpForceVisual, enemy.transform.position, Quaternion.identity, true);
                currentJumpTime = enemy.bossAttackData.maxJumpTime;
                currentJumpCount++;
                Debug.Log($"{currentJumpCount} && {enemy.bossAttackData.maxJumpCount}");
            }
        }
        public void Wave()
        {
            GameObject obj =  enemy.WaveAttackVisual(enemy.bossAttackData.jumpForceVisual, enemy.bossAttackData.waveParant.position, Quaternion.identity, enemy.bossAttackData.waveParant);
            WaveGameObject = obj;
        }
    }
}
