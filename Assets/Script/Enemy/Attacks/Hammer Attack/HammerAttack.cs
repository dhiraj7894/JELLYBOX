using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class HammerAttack : MonoBehaviour
    {
        public bool isHammerAttackComplete = false;
        public GameObject Ball;
        public float forwardForce = 5;
        public float upForce = 5;
        public void GetHammerInfo()
        {
            isHammerAttackComplete = true;
            DestroyObject();
        }

        public void Launch()
        {
            Ball.GetComponent<Rigidbody>().isKinematic = false;
            Ball.GetComponent<Rigidbody>().AddForce(Ball.transform.forward * forwardForce, ForceMode.Impulse);
            Ball.GetComponent<Rigidbody>().AddForce(Ball.transform.up * upForce, ForceMode.Impulse);
        }

        public void DestroyObject()
        {

            LeanTween.scale(Ball, new Vector3(0, 0, 0), 5).setOnComplete(() => { Destroy(this.gameObject); });
        }
    }
}