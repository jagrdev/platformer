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
        [SerializeField] private LayerCheck layerCheck;

        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int IsGround = Animator.StringToHash("is-ground");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");

        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private HeroMovements _heroMovements;

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

        private void FixedUpdate()
        {
            _heroMovements.Move(speed, jumpForce);
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
        public void Move(float speed, float jumpForce)
        {
            IsGrounded = _layerCheck.IsTouchingLayer;
            IsJumping = Motion.y > 0;
            if (IsGrounded && IsJumping)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            _rigidbody2D.velocity = GetVelocity(speed);
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