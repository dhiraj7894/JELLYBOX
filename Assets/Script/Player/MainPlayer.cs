using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;
using UnityEngine.VFX;

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
        public P_SpecialAttackCutScene SPECIALATTACK;
        #endregion

        
        [Range(0, 1)] public float playerSpeedDamp = 0.1f;
        [Range(0, 1)] public float turnSmoothDamp = 0.1f;
        [Range(0, 10)] public int enemyCheckingRange = 1;


        public CharacterController controller;
        public PlayerInput playerInput;
        public Animator anim;
        public Transform cameraTransform;
        public Rigidbody rb;

        [Space(5)]
        public Collider[] nearByEnemy;
        public Transform targetedEnemy;
        public LayerMask enemyLayerMask;

        [Space(10)]
        public float currentStamina = 10;
        public float staminaSpeed = 5;
        public float playerSpeed = 15;
        public float dashSpeed = 30;
        public float dashTime = .2f;
        public float sprintSpeedMultiplier = 4;
        public float jumpForce = 15;
        public float gravityMultiplier = 3.0f;

        [Space(10)]
        public bool isCooldown;
        public bool isStaminaCoolDown=false;
        public bool isUsableStaminaRestored = false;
        public bool isShieldActivated = false;
        public bool isSpecialAttackCooldown = false;
        public bool isDead;

        [Space(5)]
        public bool isSpecialAttack_A_CanBePerforme = false;
        public bool isSpecialAttack_B_CanBePerforme = false;
        
        [Space(10)]
        public PlayerStats stats;

        public ParticleSystem dashParticle;
        public ParticleSystem jumpParticle;
        public VisualEffect shieldParticle;
        private void Start()
        {
            IDLE = new P_Idle(this);
            SPRINT = new P_Sprint(this);
            ATTACKING = new P_Attack(this);
            HEAVYATTACK = new P_HeavyAttack(this);
            SPECIALATTACK = new P_SpecialAttackCutScene(this);
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
        }
        public void doDash(float dashMultiplayer = 1)
        {
            if(CurrrentState != null)
            {
                dashParticle.Play();
                StartCoroutine(_currentState.Dash(dashSpeed * dashMultiplayer, dashTime));
            }
        }
        public void SheildCountDown()
        {
            stats.GetShieldVFXLifetime();
            StartCoroutine(stats.ShieldReset());
        }

        IEnumerator SPAPerforme()
        {
            yield return new WaitForSeconds(stats.stats.SpecialAttackACooldownTime);
        }



        public void SPA()
        {
            StartCoroutine(stats.RefielSpecialA());
        }
        public void SPB()
        {
            stats.RefielSpecialB();
        }
        public static float GetPercentageOfValue(float num, float percentage)
        {
            float _percentage = (num*percentage)/100;
            return _percentage;
        }
    }
}
