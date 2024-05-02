using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public abstract class E_Base
    {
        public MainEnemy enemy;
        protected float _dashDistance = 0f;
        public E_Base(MainEnemy M_enemy)
        {
            enemy = M_enemy;
        }

        public virtual void EnterState()
        {
            _dashDistance = enemy.dashDistanceChecker;
        }

        public virtual void LogicUpdateState()
        {
            _dashDistance = enemy.dashDistanceChecker;
        }

        public virtual void LateLogicUpdateState()
        {

        }
        public virtual void ExitState() 
        {

        }


        public void Dash(int id)
        {
            if (id == 1)
            {
                if (Physics.Raycast(enemy.raycastCheckerTransform.position, enemy.raycastCheckerTransform.forward, out RaycastHit hit, _dashDistance - 10, enemy.ignoreLayer))
                {
                    enemy.ChangeCurrentState(enemy.COMBAT);
                }
                else
                {
                    Debug.Log("NONE . . .");
                    enemy.doDash(1,id);
                }
            }
            else if (id == -1)
            {
                if (Physics.Raycast(enemy.raycastCheckerTransform.position, -enemy.raycastCheckerTransform.forward, out RaycastHit hit, _dashDistance, enemy.ignoreLayer))
                {
                    enemy.ChangeCurrentState(enemy.COMBAT);

                }
                else
                {
                    enemy.doDash(1, id);
                }
            }
        }

        public void LookAtTarget()
        {
            if (enemy.target)
            {
                enemy.transform.LookAt(new Vector3(enemy.target.position.x, enemy.transform.position.y, enemy.target.position.z));
            }
        }

    }
}