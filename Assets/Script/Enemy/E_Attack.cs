using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    public class E_Attack : E_Base
    {
        public E_Attack(MainEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        float delay;

        public override void EnterState()
        {
            base.EnterState();

            delay = enemy.fireRate;
            enemy.SwitchPhysics(true);
        }



        public override void LogicUpdateState()
        {
            base.LogicUpdateState();


            delay -= Time.deltaTime;
            Vector3 direction = (enemy.target.transform.position - enemy.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * enemy.rotSpeed);
            float? angle = Rotate();

            if (angle != null && delay <= 0.0f)
            {
                Shoot();
                delay = enemy.fireRate;

                if (CheckTargetDistance(enemy.target) < enemy.stats.AttackRadius)
                {

                }
                else
                {
                    enemy.ChangeCurrentState(enemy.COMBAT);
                }
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        void Shoot()
        {

        }

        float? Rotate()
        {

            float? angle = CalculateAngle(false, enemy.ProjectileSpeed);

            if (angle != null)
            {
                enemy.transform.localEulerAngles = new Vector3(360.0f - (float)angle, 0.0f, 0.0f);
            }
            return angle;
        }

        float? CalculateAngle(bool low, float speed)
        {

            Vector3 targetDir = enemy.target.transform.position - enemy.transform.position;
            float y = targetDir.y;
            targetDir.y = 0.0f;
            float x = targetDir.magnitude - 1.0f;
            float gravity = 9.8f;
            float sSqr = speed * speed;
            float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

            if (underTheSqrRoot >= 0.0f)
            {

                float root = Mathf.Sqrt(underTheSqrRoot);
                float highAngle = sSqr + root;
                float lowAngle = sSqr - root;

                if (low) return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
                else return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
                return null;
        }
    }
}