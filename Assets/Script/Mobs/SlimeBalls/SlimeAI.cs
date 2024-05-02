using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Progress;


namespace Jelly.Mobs.Slime
{
    public class SlimeAI : MonoBehaviour
    {
        public SlimeController controller;
        [Header("Wayponts")]
        public Transform targetPoint;
        public Collider[] nearByWayPoints;
        public LayerMask wayPoint;
        public float CheckingRadius;

        private void Update()
        {
            if (controller.target.CompareTag(TagHash.PLAYER))
                return;
        }

        IEnumerator CheckPlayerNearBy()
        {
            while (true)
            {
                yield return new WaitForSeconds(.2f);
                nearByWayPoints = Physics.OverlapSphere(transform.position, CheckingRadius, wayPoint);
                if (nearByWayPoints.Length > 0)
                {
                    targetPoint = nearByWayPoints[0].transform;
                }
                else
                {
                    targetPoint = null;
                }

            }
        }

    }
}           