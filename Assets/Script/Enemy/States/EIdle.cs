using Jelly.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class EIdle : EBase
    {
        public EPursue pursue;
        public LayerMask detectionLayer;
        public override EBase Tick(EManager manager, EStats stats, EAnimeManager anim)
        {
            //Look for target
            Collider[] colliders = Physics.OverlapSphere(transform.position, manager.detectionRadius, detectionLayer);
            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    PlayerStats CSM = colliders[i].GetComponent<PlayerStats>();
                    if (CSM != null)
                    {
                        //check for team id ????????????????????????????                        
                        Vector3 targetDirection = CSM.transform.position - transform.position;
                        float viableAngle = Vector3.Angle(targetDirection, transform.forward);

                        if (viableAngle > -manager.detectionAngle && viableAngle < manager.detectionAngle)
                        {
                            manager.currentTarget = CSM;
                        }

                    }
                }
            }
            if (manager.currentTarget != null)
            {
                return pursue;
            }
            else
            {
                return this;
            }
        }
    }
}
