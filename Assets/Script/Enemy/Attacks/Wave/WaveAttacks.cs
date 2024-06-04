 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jelly.Enemy
{
    public class WaveAttacks : MonoBehaviour
    {
        public Rigidbody Ball;
        public GameObject Laser;
        public GameObject groundHit;

        public float maxNumberOfWave = 3;
        public float currentNumberOfWave = 0;
        public bool isWaveOver = false;
        public bool isWaveComplete = false;

        public float maxWaitTime = 3;
        private float currentWaitTime = 0;

        Vector3 hitPosition;
        GameObject hit;
        public void Start()
        {
            CheckForGround();
            LeanTween.scaleY(Laser, 3.5f, .5f).setOnComplete(() => {
                hit = Instantiate(groundHit, hitPosition, Quaternion.Euler(-90,0,0));
            });
            currentWaitTime = maxWaitTime;
        }

        private void Update()
        {
            if(currentWaitTime > 0)
            {
                currentWaitTime -= Time.deltaTime;
                if(currentWaitTime<=0)
                {
                    SpwanBall();
                    currentWaitTime = maxWaitTime;
                }
            }
        }

        public void SpwanBall()
        {
            if (!isWaveComplete && currentNumberOfWave < maxNumberOfWave)
            {
                currentNumberOfWave += 1;
                Rigidbody ball = Instantiate(Ball, transform.position, Quaternion.identity);
                if (currentNumberOfWave >= maxNumberOfWave)
                {
                    LeanTween.delayedCall(1.5f, () => {
                        LeanTween.scaleY(Laser, 0, .25f).setOnComplete(() =>
                        {
                            isWaveOver = true;
                            Destroy(this.gameObject,.5f);
                            Destroy(hit,.5f);
                        });
                    });
                    isWaveComplete = true;
                }
            }
        }
        
        void CheckForGround()
        {
            if(Physics.Raycast(transform.position,-transform.up, out RaycastHit hit, Mathf.Infinity))
            {
                hitPosition = hit.point;
            }
        }
    }
}
