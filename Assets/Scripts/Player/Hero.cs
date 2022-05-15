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

        private Vector3 _motion;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Признак, что персонаж находится на поверхности, с которой может прыгать
        /// </summary>
        private bool _isGrounded;

        /// <summary>
        /// Признак, что персонаж прыгнул и находится в полете
        /// </summary>
        private bool _isJumping;
        
        /// <summary>
        /// Можно ли сделать двойной прыжок
        /// </summary>
        private bool _canDoubleJump;

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
        }

        /// <summary>
        /// Перемещение по горизонтали
        /// </summary>
        /// <param name="motion">Отрицательное значение двигает влево, положительное вправо</param>
        public void SetMotion(Vector3 motion)
        {
            _motion = motion;
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
            Move();
            CorrectSpriteDirection(_rigidbody2D.velocity.x);
            SelectAnimation(_rigidbody2D.velocity);
        }

        private void Move()
        {
            _isGrounded = layerCheck.IsTouchingLayer;
            _isJumping = _motion.y > 0;
            if (_isGrounded && _isJumping)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            _rigidbody2D.velocity = GetVelocity();
        }

        private Vector2 GetVelocity()
        {
            var x = GetXVelocity();
            var y = GetYVelocity();

            return new Vector2(x, y);
        }

        private float GetXVelocity() => _motion.x * speed;
        
        private float GetYVelocity()
        {
            var velocity = _rigidbody2D.velocity;
            var y = velocity.y;
            if (y > 0 && !_isJumping)
            {
                y /= 2;
            }

            return y;
        }

        private void CorrectSpriteDirection(float direction)
        {
            if (direction == 0) return;
            _spriteRenderer.flipX = direction < 0;
        }

        private void SelectAnimation(Vector2 velocity)
        {
            _animator.SetBool(IsRunning, velocity.x != 0);
            _animator.SetBool(IsGround, _isGrounded);
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
}