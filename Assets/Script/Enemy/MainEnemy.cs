using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Jelly.Enemy
{
    public class MainEnemy : MonoBehaviour
    {
        #region States
        E_Base _currentState;        
        public E_Idle IDLE;
        public E_JumpWaveAttack JATTACK;
        public E_MissileLaunchAttack MATTACK;
        public E_CircleAttack CATTACK;
        public E_HammerAttack HATTACK;

        public List<E_Base> AttackList = new List<E_Base>();
        #endregion

        public float speed;

        public Transform target;
        public GameObject currentAttackVFX;

        public bool isGrounded;

        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public NavMeshAgent agent;

        [Header("Jump Wave Attack"), Space(5)]
        public float maxJumpTime = 1;
        public int maxJumpCount;
        public AnimationCurve jumpWaveCurve;
        public GameObject jumpForceVisual;

        [Header("Missile Attack"), Space(5)]
        public GameObject iceShard;

        [Header("Circle Attack"), Space(5)]
        public AnimationCurve speedWaveCurve;
        public AnimationCurve circleWaveCurve;
        public GameObject circleAttackVisual;

        [Header("Hammer Attack"), Space(5)]
        public AnimationCurve hammerWaveCurve;
        public GameObject hammerAttackVisual;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            StateInitialize();
            _currentState = IDLE;
            _currentState.EnterState();
        }

        public void StateInitialize()
        {
            IDLE = new E_Idle(this);
            JATTACK = new E_JumpWaveAttack(this);
            MATTACK = new E_MissileLaunchAttack(this);
            CATTACK = new E_CircleAttack(this);
            HATTACK = new E_HammerAttack(this);

            AttackList.Add(JATTACK); 
            AttackList.Add(MATTACK);
            AttackList.Add(CATTACK);
            AttackList.Add(HATTACK);

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


        public void ShowAttackVisual(GameObject attackVisual, Vector3 pos, Quaternion quaternion)
        {
            if(!currentAttackVFX)currentAttackVFX = Instantiate(attackVisual, pos, quaternion);
        }


    }
}