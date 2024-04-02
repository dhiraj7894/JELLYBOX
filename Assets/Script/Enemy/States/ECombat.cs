using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class ECombat : EBase
    {
        public EBase[] attacks;
        public EPursue pursue;


        public EBase currentAttack;
        public EBase previousAttack;
        public override EBase Tick(EManager manager, EStats stats, EAnimeManager anim)
        {
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, manager.transform.position);

            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);
            manager.viableAngle = Vector3.Angle(targetDirection, transform.forward);
            Attacks();
            if (currentAttack)
            {
                return currentAttack;
            }
            else if (manager.distanceFromTarget > manager.maximumAttackRange)
            {
                return pursue;
            }
            else
            {
                return this;
            }
        }

        public void Attacks()
        {
            
            if(currentAttack) previousAttack = currentAttack;
            if(previousAttack != null)
            {
                if(previousAttack == currentAttack)
                {
                    Attacks();
                }
            }
            else
            {
                currentAttack = attacks[0];
            }
        } 
    }
}
