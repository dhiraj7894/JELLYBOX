using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

namespace Jelly.Enemy
{
    public class EPursue : EBase
    {
        public EIdle idle;
        public ECombat combat;
        public LayerMask detectionLayer;
        
        float turnSmoothVelocity;
        public override EBase Tick(EManager manager, EStats stats, EAnimeManager anim)
        {
            if (manager.isPerformingAction)
                return this;
            if (!manager.agent.enabled) manager.agent.enabled = true;
            Vector3 targetDirection = manager.currentTarget.transform.position - transform.position;
            manager.distanceFromTarget = Vector3.Distance(manager.currentTarget.transform.position, transform.position);
            manager.viableAngle = Vector3.Angle(targetDirection, transform.forward);
            if (manager.distanceFromTarget > manager.maximumAttackRange)
            {
                manager.agent.SetDestination(manager.currentTarget.transform.position);
                //anim.anim.SetFloat(AnimHash.VERTICAL, 1, 0.1f, Time.deltaTime);
            }
            HandleRotation(manager);
            //manager.agent.transform.localPosition = Vector3.zero;
            //manager.agent.transform.localRotation = Quaternion.identity;
            // Check the distance between then choose either Idle or Combat


            if (manager.currentTarget)
            {
                if (manager.distanceFromTarget <= manager.maximumAttackRange)
                {
                    return combat;
                }
            }
            else if (manager.currentTarget == null)
            {
                return idle;
            }
            else
            {
                return this;
            }
            return null;
        }

        public void HandleRotation(EManager manager)
        {
            Vector3 direction = new Vector3(manager.agent.velocity.x, 0, manager.agent.velocity.z).normalized;
            if(direction.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(manager.transform.position.y, targetAngle, ref turnSmoothVelocity, manager.rotationSpeed);
                manager.transform.rotation = Quaternion.Euler(0f,angle, 0f);
            }
        }



        public void HandleRotationToTarget(EManager manager)
        {
            if (manager.isPerformingAction)
            {
                Vector3 direction = manager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                manager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, manager.rotationSpeed / Time.deltaTime);
            }
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(manager.agent.desiredVelocity);
                Vector3 targetVelocity = manager.enemyRB.velocity;

                manager.agent.enabled = true;
                manager.agent.SetDestination(manager.currentTarget.transform.position);
                manager.enemyRB.velocity = targetVelocity;
                manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, manager.agent.transform.rotation, manager.rotationSpeed / Time.deltaTime);
            }


        }
    }
}

