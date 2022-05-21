using System;
using Components;
using Model;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Описывает поведение главного персонажа
    /// </summary>
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed = 3.0f;
        [SerializeField] private float jumpForce = 1.0f;
        [SerializeField] private float _damageJumpForce = 1.0f;
        [SerializeField] private LayerCheck layerCheck;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;

        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int IsGround = Animator.StringToHash("is-ground");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int HasDamage = Animator.StringToHash("damage");
        private static readonly int HasHealth = Animator.StringToHash("heal");

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private HeroMovements _heroMovements;
        private readonly Collider2D[] _interactionResult = new Collider2D[1];

        /// <summary>
        /// Карман с предметами
        /// </summary>
        private Pocket _pocket;

        private void Awake()
        {
            _pocket = new Pocket();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _heroMovements = new HeroMovements(layerCheck, _rigidbody2D);
        }

        /// <summary>
        /// Перемещение по горизонтали
        /// </summary>
        /// <param name="motion">Отрицательное значение двигает влево, положительное вправо</param>
        public void SetMotion(Vector3 motion)
        {
            _heroMovements.Motion = motion;
        }

        public void PutSilverCoin()
        {
            _pocket.PutSilverCoin();
            ShowMoneyInThePocket();
        }

        public void PutGoldenCoin()
        {
            _pocket.PutGoldenCoin();
            ShowMoneyInThePocket();
        }
        
        /// <summary>
        /// Получение повреждения
        /// </summary>
        public void TakeDamage()
        {
            _animator.SetTrigger(HasDamage);
            _heroMovements.DamageJump(_damageJumpForce);
        }
        
        /// <summary>
        /// Получение повреждения
        /// </summary>
        public void TakeHealth()
        {
            _animator.SetTrigger(HasHealth);
        }

        private void FixedUpdate()
        {
            _heroMovements.Jump(speed, jumpForce);
            CorrectSpriteDirection(_rigidbody2D.velocity.x);
            SelectAnimation(_rigidbody2D.velocity);
        }

        private void CorrectSpriteDirection(float direction)
        {
            if (direction == 0) return;
            _spriteRenderer.flipX = direction < 0;
        }

        private void SelectAnimation(Vector2 velocity)
        {
            _animator.SetBool(IsRunning, velocity.x != 0);
            _animator.SetBool(IsGround, _heroMovements.IsGrounded);
            _animator.SetFloat(VerticalVelocity, velocity.y);
        }

        private void ShowMoneyInThePocket()
        {
            var points = _pocket.SilverCoins + _pocket.GoldenCoins * 10;
            Debug.Log($"All points: {points}\t" +
                      $"Silver: {_pocket.SilverCoins}\t" +
                      $"Golden: {_pocket.GoldenCoins}");
        }

        public void Interact()
        {
            var hit = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer);

            foreach (var interact in _interactionResult)
            {
                var interactable = interact.GetComponent<InteractableComponent>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    /// <summary>
    /// Определяет логику движения персонажа(бег, прыжки, падения)
    /// </summary>
    internal class HeroMovements
    {
        /// <summary>
        /// Вектор направления движения персонажа
        /// </summary>
        public Vector3 Motion { get; set; }

        /// <summary>
        /// Признак, что персонаж находится на поверхности, с которой может прыгать
        /// </summary>
        public bool IsGrounded { get; private set; }

        /// <summary>
        /// Признак, что персонаж находится в прыжке вверх
        /// </summary>
        private bool IsJumping { get; set; }

        private bool IsFalling { get; set; }

        /// <summary>
        /// Определяет, можно ли совершить двойной прыжок
        /// </summary>
        private bool CanDoubleJump
        {
            get => !IsGrounded && IsFalling && IsJumping && !_wasDoubleJumped;
            set => _wasDoubleJumped = !value;
        }

        /// <summary>
        /// Признак, был ли совершен двойной прыжок
        /// </summary>
        private bool _wasDoubleJumped;

        private readonly LayerCheck _layerCheck;
        private readonly Rigidbody2D _rigidbody2D;

        public HeroMovements(LayerCheck layerCheck, Rigidbody2D rigidbody2D)
        {
            _layerCheck = layerCheck;
            _rigidbody2D = rigidbody2D;
        }

        /// <summary>
        /// Перемещает персонажа
        /// </summary>
        /// <param name="speed">Скорость перемещения по горизонтали</param>
        /// <param name="jumpForce">Сила импульса при прыжке</param>
        public void Jump(float speed, float jumpForce)
        {
            IsGrounded = _layerCheck.IsTouchingLayer;
            if (IsGrounded) CanDoubleJump = true;
            IsJumping = Motion.y > 0;
            IsFalling = _rigidbody2D.velocity.y < 0.01;
            MakeJump(jumpForce);
            MakeDoubleJump(jumpForce);
            _rigidbody2D.velocity = GetVelocity(speed);
        }
        
        public void DamageJump(float velocity)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);
        }

        private void MakeJump(float velocity)
        {
            if (IsGrounded && IsJumping)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);
            }
        }

        private void MakeDoubleJump(float velocity)
        {
            if (CanDoubleJump)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, velocity);
                CanDoubleJump = false;
            }
        }

        private Vector2 GetVelocity(float speed)
        {
            var x = Motion.x * speed;
            var y = InterruptJump(_rigidbody2D.velocity.y);

            return new Vector2(x, y);
        }

        private float InterruptJump(float y)
        {
            if (y > 0 && !IsJumping)
            {
                y /= 2;
            }

            return y;
        }
    }
}