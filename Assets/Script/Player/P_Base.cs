using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Player
{
    
    public abstract class P_Base
    {
        public MainPlayer player;


        protected Vector2 _input;

        protected InputAction _moveAction;
        protected InputAction _sprintAction;        
        protected InputAction _dashAction;

        protected bool _isIdle = false;
        protected bool _isSprint = false;
        protected bool _isDash = false;

        public bool _isDead = false;
        public bool _isRevived = false;

        protected float _playerSpeed = 5;

        private float _turnSmoothVelocity;
        private float _gravity = -9.8f;
        private float _gravityMulitplier = 3.0f;

        private float _strafeSpeedMultiplier = 0.75f;
        private float _backSpeedMultiplier = 0.4f;
        protected Vector3 _velocity;
        public P_Base(MainPlayer mainPlayer)
        {
            player = mainPlayer;
        }



        public virtual void EnterState()
        {
            //Debug.Log("Current State is : " + this.ToString());
            _moveAction = player.playerInput.actions["Move"];
            _sprintAction = player.playerInput.actions["Sprint"];            
            _dashAction = player.playerInput.actions["Dash"];

            _playerSpeed = player.playerSpeed;
            _gravityMulitplier = player.gravityMultiplier;
        }


        public virtual void ManageInput() { }

        public virtual void LogicUpdateState()
        {            
            addGeavity();
            if (_dashAction.triggered)
            {
                Jump();
            }
        }

        public virtual void ExitState() { }

        public void MovementUpdate(float speed = 1)
        {
            if (!_isDead)
            {
                float targetAngle = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + player.cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, player.turnSmoothDamp);
                player.transform.rotation = Quaternion.Euler(0, angle, 0);
                Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                player.controller.Move(moveDir.normalized * _playerSpeed * speed * Time.deltaTime);
            }

        }

        public void RotateTowardCamera()
        {

            Vector3 viwDir = (player.targetedEnemy.position - player.transform.position).normalized;
            viwDir.y = 0;
            Quaternion rotation = Quaternion.LookRotation(viwDir);
            LeanTween.rotate(player.gameObject, rotation.eulerAngles, 0.1f);
        }
        public void addGeavity()
        {
            if (player.controller.isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -1f;
            }
            else
            {
                _velocity.y += _gravity * _gravityMulitplier * Time.deltaTime;
            }

            player.controller.Move(_velocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (player.controller.isGrounded)
            {
                _velocity.y += player.jumpForce;
                player.animator.Play(AnimationVeriable.JUMP);
            }
        }
    }
}

