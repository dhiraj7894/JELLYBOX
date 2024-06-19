using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class AttackParticalCollition : MonoBehaviour
    {
        public float damage = 10;
        public float maxSize = 5;
        public SphereCollider col;
        bool isDamageTaken = false;
        
        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag(TagHash.PLAYER)&& !isDamageTaken)
            {
                Debug.Log("Player Collided");
                other.GetComponent<IHealthSystem>().TakeDamage(damage);
                isDamageTaken = true;
            }
        }

        private void Start()
        {
            if(col)LeanTween.value(gameObject, UpdateValue, 1, maxSize, .2f);
        }

        void UpdateValue(float val)
        {
            col.radius = val;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.PLAYER) && !isDamageTaken)
            {
                Debug.Log("Player Trigger Collider");
                other.GetComponent<IHealthSystem>().TakeDamage(damage);
                isDamageTaken = true;
            }
        }
    }
}
