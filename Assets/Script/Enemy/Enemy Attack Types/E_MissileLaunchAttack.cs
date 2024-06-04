using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_MissileLaunchAttack : E_Base
    {

        public E_MissileLaunchAttack(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public Vector3 spwanPos;

        public override void EnterState()
        {
            spwanPos = enemy.bossAttackData.missileLauncherPosition.position;
            enemy.agent.enabled = false;
            enemy.WaveAttackVisual(enemy.bossAttackData.missiles, new Vector3(spwanPos.x, spwanPos.y + 4, spwanPos.z), Quaternion.identity);
            base.EnterState();
        }

        public override void ExitState()
        {
            enemy.agent.enabled = true;
            base.ExitState();
        }

        public override void LogicUpdateState()
        {
            if (enemy.currentAttackVFX)
            {
                try
                {
                    if (enemy.currentAttackVFX.GetComponent<IceShard>().isExplosionCompleted && enemy.isGrounded)
                    {
                        // Change to combat state
                        enemy.ChangeCurrentState(enemy.IDLE);
                    }
                }
                catch
                {

                }
            }
            base.LogicUpdateState();
        }
    }
}
