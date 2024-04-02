using Jelly.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Jelly.Enemy
{
    public class EManager : CharacterManager
    {
        public EAnimeManager eAnimeManager;
        public EStats eStats;

        public NavMeshAgent agent;
        public Rigidbody enemyRB;


        public EBase currentState;

        public bool isPerformingAction;
        public bool isGrounded;

        [Header("A.I. Setting")]
        public PlayerStats currentTarget;

        public float distanceFromTarget;
        public float rotationSpeed = 15;
        public float maximumAttackRange = 1.25f;
        public float detectionRadius = 30;
        public float viableAngle = 50;
        public float detectionAngle = 50;

        public float currentRecoveryTime = 0;


        private void Start()
        {
            eAnimeManager = GetComponent<EAnimeManager>();
            eStats = GetComponent<EStats>();
            agent = GetComponentInChildren<NavMeshAgent>();
            enemyRB = GetComponent<Rigidbody>();

            agent.enabled = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();
            HandleStateMachine();
        }

        private void FixedUpdate()
        {
            
        }

        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                EBase nextState = currentState.Tick(this, eStats, eAnimeManager);
                if (nextState != null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(EBase state)
        {
            currentState = state;
        }

        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction)
            {
                if (currentRecoveryTime < 0)
                {
                    isPerformingAction = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(TagHash.GROUND))isGrounded = true;
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagHash.GROUND)) isGrounded = false;
        }
    }
}
