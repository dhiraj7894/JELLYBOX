using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jelly.Enemy
{
    public class AttackP1 : EBase
    {
        public ECombat combat;
        public bool isAttacking;
        public bool isAttackCompleted;

        EManager manager;
        EStats stats;
        EAnimeManager anime;
        public override EBase Tick(EManager manager, EStats stats, EAnimeManager anim)
        {
            if (!isAttacking)
            {
                this.manager = manager;
                this.stats = stats;
                this.anime = anim;
                isAttacking = true;
            }
            if(isAttackCompleted)
            {
                return combat;
            }
            return combat;
        }

        [Header("Jump Force")]
        public int maxJumpCount;
        public float jumpTime = 1;
        public float jumpForce = 5;

        int currentJumpCount;
        float currentJumpTime;

        [Header("Visual Effects")]
        public GameObject aVisual;

        public void TriggerJump()
        {
            if(currentJumpTime <= 0) {
                manager.enemyRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                currentJumpTime = jumpTime;
            }
        }

        
        private void Update()
        {
            if(isAttacking && currentJumpTime > 0)
            {
                currentJumpTime -= Time.deltaTime;
                TriggerJump();
                if(manager.isGrounded)
                {
                    SpwanVisual();
                }
            }
        }

        public void SpwanVisual()
        {
            currentJumpCount++;
            Instantiate(aVisual);
        }

    }
}
