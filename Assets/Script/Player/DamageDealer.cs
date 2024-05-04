using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Player
{
    public class DamageDealer : MonoBehaviour
    {
        public SwordStatsSO currentSwordSO;
        public CinemachineImpulseSource source;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.ENEMY))
            {
                //Debug.Log("Enemy Tagged");
                other.GetComponent<IHealthSystem>().TakeDamage(currentSwordSO.Damage);
                source.GenerateImpulse(Camera.main.transform.forward);
            }

        }
    }
}
