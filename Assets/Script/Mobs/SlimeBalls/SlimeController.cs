using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Jelly.Mobs.Slime
{
    public class SlimeController : MonoBehaviour
    {
        [Header("Slime Control")]
        public Transform target;
        public Rigidbody body;

        public float moveSpeed = 5;
        public float jumpForce = 10;
        public float jumpTime = .5f;
        public float jumpTimerSpeed = 2;
        public float torqueForce = 1;
        public bool isGrounded = false;
        public bool isJumping = true;

        [Header("Slime Pretrol")]
        public bool isPlayerNear = false;
        public bool isSlimeBlast = false;
        public float CheckingRadius = 5;
        public Collider[] nearByCollider;
        public LayerMask checkingLayer;

        [Header("Slime Explode")]
        public float explosionSize;
        public Animator anim;
        public void JumpMove()
        {
            if (isGrounded && !isJumping)
            {
                body.AddForce(Vector3.up * jumpForce);
                body.AddForce(transform.forward * moveSpeed);
            }
        }

        private float waitTimeJump;
        private float Distance = 5;
        public void JumpCheck()
        {
            if(isGrounded)
            {
                waitTimeJump -= jumpTimerSpeed * Time.deltaTime;
                if(waitTimeJump < 0)
                {
                    isJumping = false;
                }

                if (target)
                {
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                }
            }
            if (target)
            {
                if (Vector3.Distance(transform.position, target.position) <= Distance)
                {
                    isSlimeBlast = true;
                    LeanTween.delayedCall(1.0f, () => { Explode(); });
                }
            }
        }
        private void Update()
        {
            if (isSlimeBlast) return;
            JumpMove();
            JumpCheck();
        }

        public void Explode()
        {
            // Explode animation
            anim.Play("Explode");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.GROUND))
            {
                waitTimeJump = jumpTime;
                isGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagHash.GROUND))
            {
                isGrounded = false;
                isJumping = true;
            }
        }

        void FixedUpdate()
        {
            // Calculate torque to keep the cube balanced
            Vector3 torque = Vector3.Cross(transform.up, Vector3.up) * torqueForce;

            // Apply torque to the cube
            body.AddTorque(torque);
        }
        IEnumerator CheckPlayerNearBy()
        {
            yield return null;
            while (!isPlayerNear)
            {
                yield return new WaitForSeconds(.2f);
                nearByCollider = Physics.OverlapSphere(transform.position, CheckingRadius, checkingLayer);
                if (nearByCollider.Length > 0)
                {
                    foreach (var item in nearByCollider)
                    {
                        if (item.CompareTag(TagHash.PLAYER))
                        {
                            target = item.transform;
                        }
                    }
                }
                else
                {
                    target = null;
                }
                
            }
        }

        private void OnEnable()
        {
            //Radar to check near by player
            StartCoroutine(CheckPlayerNearBy());
        }

        private void OnDisable()
        {
            StopCoroutine(CheckPlayerNearBy());
        }
    }
}