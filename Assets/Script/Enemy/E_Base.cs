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
            KnockDown();
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

        public void LookAtTarget(float duration)
        {
            if (enemy.target)
            {
                //enemy.transform.LookAt(new Vector3(enemy.target.position.x, enemy.transform.position.y, enemy.target.position.z));
                Vector3 direction = enemy.target.position - enemy.transform.position;

                // Create the rotation we need to be in to look at the target
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Start the LeanTween rotation
                LeanTween.rotate(enemy.gameObject, targetRotation.eulerAngles, duration).setEase(LeanTweenType.easeInOutSine);
            }
            
        }
        public void GetKnockData(float val)
        {
            enemy.currentKnockTime = val;
        }
        public void KnockDown()
        {
            if (enemy.currentKnockTime > 0)
            {
                enemy.currentKnockTime -= Time.deltaTime;
                if (enemy.currentKnockTime <= 0)
                {
                    enemy.ChangeCurrentState(enemy.KNOCKOUT);
                }
            }
        }
    }
}