using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

namespace Jelly.Enemy
{
    [Serializable]
    public class BossAttackData
    {
        [Header("Jump Wave Attack"), Space(5)]
        public float maxJumpTime = 1;
        public int maxJumpCount;
        public Transform waveParant;
        public AnimationCurve jumpWaveCurve;
        public GameObject jumpForceVisual;

        [Header("Missile Attack"), Space(5)]
        public GameObject missiles;
        public Transform missileLauncherPosition;

        [Header("Circle Attack"), Space(5)]
        public AnimationCurve speedWaveCurve;
        public AnimationCurve circleWaveCurve;
        public GameObject circleAttackVisual;
        public Transform circleparant;

        [Header("Hammer Attack"), Space(5)]
        public Transform hammerAttackParant;
        public AnimationCurve hammerWaveCurve;
        public GameObject hammerAttackVisual;

        [Header("Basic Attacks"), Space(5)]
        public float forceMultiplyer;
        public Rigidbody bombBall;
        public Transform[] bombThrowerPosition;
    }
    public class MainEnemy : MonoBehaviour
    {
        public string currentStats; 
        #region States
        E_Base _currentState;
        public E_Prepare PREP;
        public E_Idle IDLE;
        public E_Pursuit PURSUIT;
        public E_Combat COMBAT;
        public E_KnockOut KNOCKOUT;

        public E_JumpWaveAttack JATTACK;
        public E_MissileLaunchAttack MATTACK;
        public E_CircleAttack CATTACK;
        public E_HammerAttack HATTACK;

        public List<E_Base> AttackList = new List<E_Base>();
        #endregion

        public GameObject currentAttackVFX;
        [Space(5)]
        public Transform target;
        public Transform attackVisialParant;
        public Transform raycastCheckerTransform;
        public Transform enemyGFX;
        [Space(5)]
        public Animator anim;
        public Rigidbody rb;
        public NavMeshAgent agent;
        public EStats stats;
        [Space(5)]
        public bool isGrounded;
        public bool isDead = false;
        public bool isCutSceneRunning = true;

        [Header("EnemyControlls")]
        [Space(5)]
        public float distanceFromTarget = 0;
        public LayerMask ignoreLayer;
        [Space(5)]
        public float speed;
        public int currentAttackPattern = 0;
        public float dashForwardDistance = 2;
        public float backOffDistance = .5f;
        public float pursuitDistance = 4;
        public float dashDistanceChecker = 15;
        [Space(5)]
        public float dashSpeed = 10;
        public float dashTime = 1;

        [Header("Knockout"), Space(10)]
        public float currentKnockTime = 0;
        public float maxKnockOutDuration = 5;
        public float durationOfKnockTime = 5;

        [Header("UI Controls")]
        public GameObject UI;


        public bool isDashBackward = false;
        public bool isSecondPhase = false;

        public BossAttackData bossAttackData;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            StateInitialize();
            _currentState = PREP;
            _currentState.EnterState();
        }

        public void StateInitialize()
        {
            PREP = new E_Prepare(this);
            IDLE = new E_Idle(this);
            PURSUIT = new E_Pursuit(this);
            COMBAT = new E_Combat(this);
            KNOCKOUT = new E_KnockOut(this);
            JATTACK = new E_JumpWaveAttack(this);
            MATTACK = new E_MissileLaunchAttack(this);
            CATTACK = new E_CircleAttack(this);
            HATTACK = new E_HammerAttack(this);

            AttackList.Add(HATTACK); 
            AttackList.Add(JATTACK);
            AttackList.Add(MATTACK);
            AttackList.Add(CATTACK);

        }

        private void Update()
        {
            if (stats.health.currentHealth<=0 && !isDead)
            {
                anim.Play("Dead");
                isDead = true;  
            }
            if (isDead)
                return;

            _currentState.LogicUpdateState();
            DistanceChecker();
            currentStats = _currentState.ToString();
        }

        private void LateUpdate()
        {
            _currentState.LateLogicUpdateState();
        }
        public void ChangeCurrentState(E_Base newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void DistanceChecker()
        {
            if (target)
            {
                distanceFromTarget = Vector3.Distance(transform.position, target.position);
            }
            else
            {
                distanceFromTarget = 0;
            }

        }

        public void ShowAttackVisual(GameObject attackVisual, Vector3 pos, Quaternion quaternion, Transform isParant, bool multiSpwan = false)
        {
            if (multiSpwan)
            {
                currentAttackVFX = Instantiate(attackVisual, pos, quaternion);
            }
            else
            {
                if (!currentAttackVFX) currentAttackVFX = Instantiate(attackVisual, pos, quaternion);
            }

            if (isParant)
            {
                currentAttackVFX.transform.parent = isParant;
                currentAttackVFX.transform.localEulerAngles = Vector3.zero;
            }

        }

        public float RandomNumberGenrator(float min, float max)
        {
            float x = UnityEngine.Random.Range(min, max);
            return x;
        }

        public Rigidbody BombBallThrow()
        {
            Rigidbody rb = Instantiate(bossAttackData.bombBall, bossAttackData.bombThrowerPosition[UnityEngine.Random.Range(0, bossAttackData.bombThrowerPosition.Length)].position + new Vector3(0,0.5f,0), Quaternion.identity);
            return rb;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagHash.GROUND))
                isGrounded = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagHash.GROUND))
                isGrounded = false;
        }

        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawLine(raycastCheckerTransform.position, raycastCheckerTransform.position + raycastCheckerTransform.forward * dashDistanceChecker);
        }
        public void doDash(float dashMultiplayer, int id)
        {
            if (id == 1) StartCoroutine(Dash(transform.forward, dashSpeed * dashMultiplayer, dashTime)); 
            else if (id == -1) StartCoroutine(Dash(-transform.forward, dashSpeed * dashMultiplayer, dashTime));

        }
        public IEnumerator Dash(Vector3 input, float dashSpeed, float dashTime)
        {

            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                transform.Translate(input * dashSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void SetCutSceneBoolenValue()
        {
            isCutSceneRunning = false;
        }

        public void GFXHEightManager(float height, float time = 0.5f)
        {
            LeanTween.moveLocalY(enemyGFX.gameObject, height, time);
        }
        

        public GameObject WaveAttackVisual(GameObject Visual, Vector3 pos, Quaternion quaternion, Transform parant = null)
        {
            if (parant)
            {
                GameObject obj = Instantiate(Visual, pos, quaternion, parant);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(Visual, pos, quaternion);
                return obj;
            }
            
        }
    }
}