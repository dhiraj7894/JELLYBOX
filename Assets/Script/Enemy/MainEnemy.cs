using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Enemy
{
    public class MainEnemy : MonoBehaviour
    {
        #region States
        E_Base _currentState;
        public E_Idle IDLE;
        public E_Petrol PETROL;
        public E_Chase CHASE;
        public E_Combat COMBAT;
        public E_Attack ATTACK;
        #endregion

        public float speed;
        public Transform target;
        public EnemyStats stats;

        [Header("Petrol")]
        public int CurrentWaypointIndex = 0;
        public List<Transform> PetrolWaypoints = new List<Transform>();
        public float DeltaTouchDistance = 2;

        [Header("Attack")]
        public bool isFromAbove;
        public float ProjectileSpeed;
        public Transform shootPos;


        [HideInInspector] public FieldOfView fieldOfView;

        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public NavMeshAgent agent;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            IDLE = new E_Idle(this);
            PETROL = new E_Petrol(this);
            CHASE = new E_Chase(this);
            COMBAT = new E_Combat(this);
            ATTACK = new E_Attack(this);

            _currentState = IDLE;
            _currentState.EnterState();
        }

        private void Update()
        {
            _currentState.LogicUpdateState();
        }

        public void ChangeCurrentState(E_Base newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }


        public void SwitchPhysics(bool status)
        {
            rb.isKinematic = status;
            agent.enabled = !status;
        }
    }
}