using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

namespace Game.Player
{
    public class MainPlayer : MonoBehaviour
    {

        public string CurrrentState;

        #region STATES
        P_Base _currentState;
        public P_Idle IDLE;
        public P_Sprint SPRINT;
        public P_Attack ATTACKING;
        public P_HeavyAttack HEAVYATTACK;
        #endregion

        
        [Range(0, 1)] public float playerSpeedDamp = 0.1f;
        [Range(0, 1)] public float turnSmoothDamp = 0.1f;
        [Range(0, 10)] public int enemyCheckingRange = 1;


        public CharacterController controller;
        public PlayerInput playerInput;
        public Animator anim;
        public Transform cameraTransform;
        public Transform targetedEnemy;
        public Rigidbody rb;

        public Collider[] nearByEnemy;
        public LayerMask enemyLayerMask;

        public float currentStamina = 10;
        public float staminaSpeed = 5;
        public float playerSpeed = 15;
        public float dashSpeed = 30;
        public float dashTime = .2f;
        public float sprintSpeedMultiplier = 4;
        public float jumpForce = 15;
        public float gravityMultiplier = 3.0f;

        public bool isCooldown;
        public bool isStaminaCoolDown=false;
        public bool isUsableStaminaRestored = false;
        public bool isDead;

        public PlayerStats stats;

        public ParticleSystem dashParticle;
        public ParticleSystem jumpParticle;
        private void Start()
        {
            IDLE = new P_Idle(this);
            SPRINT = new P_Sprint(this);
            ATTACKING = new P_Attack(this);
            HEAVYATTACK = new P_HeavyAttack(this);
            _currentState = IDLE;
            _currentState.EnterState();
            currentStamina = stats.stats.MaxStamina;
        }

        private void Update()
        {
            _currentState.LogicUpdateState();
            _currentState.ManageInput();
            CurrrentState = _currentState.ToString();

            if (Input.GetKeyDown(KeyCode.X))
            {
                isDead = true;
            }
        }

        public void ChangeCurrentState(P_Base newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void EnemyChecker()
        {
            if(!isDead) nearByEnemy = Physics.OverlapSphere(transform.position, enemyCheckingRange, enemyLayerMask);
            if (nearByEnemy.Length != 0)
            {
                Transform target = nearByEnemy[0].transform;
                targetedEnemy = target;
            }
            else
            {
                targetedEnemy = null;
            }
        }
        public void Cooldown()
        {
            isCooldown = true;
            
        }

        public void StartRefilStamina()
        {
            StartCoroutine(stats.StaminaRefil());
        }
        public void StopRefilStamina()
        {
            if (!isUsableStaminaRestored)
            {
                StopCoroutine(stats.StaminaRefil());
                isUsableStaminaRestored = false; 
            }
            //isStaminaCoolDown = false;
        }
        public void doDash(float dashMultiplayer = 1)
        {
            if(CurrrentState != null)
            {
                dashParticle.Play();
                StartCoroutine(_currentState.Dash(dashSpeed * dashMultiplayer, dashTime));
            }
        }

    }
}
