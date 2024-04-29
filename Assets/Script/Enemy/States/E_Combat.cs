using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jelly.Enemy
{
    [System.Serializable]
    public class E_Combat : E_Base
    {
        public float timeToAttack = 1;
        public float timeToSpecialAttack = 3;
        public float timeToPowerAttack = 3;

        public bool isSecondPhase = false;

        public int[] phaseOneAttackPattern = new int[] { 0, 0, 1, 0, 1, 0, 0 };
        public int[] phaseTwoAttackPattern = new int[] { 3, 2, 2, 4, 2, 1, 3 };

        public int currentAttack = 0;
        E_Base State = null;
        public E_Combat(MainEnemy M_enemy) : base(M_enemy)
        {
            enemy = M_enemy;
        }
        public override void EnterState()
        {
            //Ready things for attack
            timeToPowerAttack = enemy.RandomNumberGenrator(2.5f, 5f);
            timeToSpecialAttack = enemy.RandomNumberGenrator(1.5f, 3f);
            timeToAttack = enemy.RandomNumberGenrator(0.2f, 1f);

            if (!isSecondPhase) 
                currentAttack = phaseOneAttackPattern[enemy.currentAttackPattern];
           else
                currentAttack = phaseTwoAttackPattern[enemy.currentAttackPattern];

            Debug.Log("Enter State . . . .");

        }

        public override void ExitState()
        {
            if (enemy.currentAttackPattern >= phaseOneAttackPattern.Length - 1 )
                enemy.currentAttackPattern = 0;
            else enemy.currentAttackPattern ++;
        }

        public override void LogicUpdateState()
        {
            DoDash();
            GetAttackPhaseOne();
            ///
            /// Check for current HP stats
            /// If HP is less then 50% of max HP then <isSecondPhase> is true            
            ///
            
        }


        public void DoDash()
        {
           /* if (isBackOff)
            {
                Debug.Log("Back Dash");

                float randomDashTime = Random.Range(0.01f, 0.05f);
                enemy.dashTime = randomDashTime;
                Dash(-1);
                isBackOff = false;
            }*/
        }

        public void GetAttackPhaseOne()
        {
            if (isSecondPhase)
                return;

            timeToAttack -= Time.deltaTime;
            timeToSpecialAttack -= Time.deltaTime;

            if (timeToAttack <= 0 && timeToSpecialAttack>0)
            {
                LookAtTarget();
                Rigidbody rbBall = enemy.BombBallThrow();
                rbBall.AddForce(enemy.transform.forward * 100, ForceMode.Impulse);
                timeToAttack = enemy.RandomNumberGenrator(0.12f, .5f);
            }
            if (timeToSpecialAttack <= 0 && timeToAttack > 0)
            {
                enemy.ChangeCurrentState(enemy.AttackList[currentAttack]);

            }
            //This both can be executed afetr 2nd half

        }
        public void GetAttackPhaseTwo()
        {
            if (!isSecondPhase)
                return;

            timeToAttack -= Time.deltaTime;
            timeToSpecialAttack -= Time.deltaTime;
            timeToPowerAttack -= Time.deltaTime;

            if (timeToAttack <= 0 && timeToSpecialAttack > 0)
            {

            }
            if (timeToSpecialAttack <= 0 && timeToAttack > 0)
            {

            }

            //This both can be executed afetr 2nd half

        }


    }
}
