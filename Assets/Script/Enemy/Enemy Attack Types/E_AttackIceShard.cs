using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class E_AttackIceShard : E_Base
    {

        public E_AttackIceShard(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }

        public override void EnterState()
        {
            enemy.agent.enabled = false;
            enemy.ShowAttackVisual(enemy.iceShard, new Vector3(enemy.transform.position.x, enemy.transform.position.y + 4, enemy.transform.position.z), Quaternion.identity);
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
                    if (enemy.currentAttackVFX.GetComponent<IceShard>().isExplosionCompleted)
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
