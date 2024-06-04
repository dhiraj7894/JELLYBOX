using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class Hammer : MonoBehaviour
    {
        public CinemachineImpulseSource source;
        public GameObject impact;
        public bool isImpactSpwanned = false;
        public float destroyTime = 5;
        private void OnTriggerEnter(Collider other)
        {
            if (!isImpactSpwanned)
            {
                GameObject impactObj = Instantiate(impact, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity);
                Destroy(impactObj, destroyTime);
                isImpactSpwanned = true;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            source.GenerateImpulse(Camera.main.transform.forward);
        }
    }
}
