using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public abstract class E_Base
    {
        public MainEnemy enemy;

        public E_Base(MainEnemy M_enemy)
        {
            enemy = M_enemy;
        }

        public virtual void EnterState()
        {

        }

        public virtual void LogicUpdateState()
        {

        }

        public virtual void ExitState() 
        {

        }

        public float CheckTargetDistance(Transform target)
        {
            float distance = Vector3.Distance(enemy.transform.position, target.position);

            return distance;
        }
    }
}