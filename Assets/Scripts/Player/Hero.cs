using System;
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

        private static int IsRunning = Animator.StringToHash("is-running");
        private static int IsGround = Animator.StringToHash("is-ground");
        private static int VerticalVelocity = Animator.StringToHash("vertical-velocity");

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
        public void Move(Vector3 motion)
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
            _isGrounded = layerCheck.IsTouchingLayer;
            _isJumping = _motion.y > 0;
            var velocity = _rigidbody2D.velocity;
            var x = _motion.x * speed;
            var y = velocity.y;
            if (y > 0 && !_isJumping)
            {
                y /= 3;
            }

            _rigidbody2D.velocity = new Vector2(x, y);
            if (_isGrounded && _isJumping)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            UpdateSpriteDirection(x);

            _animator.SetBool(IsRunning, x != 0);
            _animator.SetBool(IsGround, _isGrounded);
            _animator.SetFloat(VerticalVelocity, y);
        }

        private void UpdateSpriteDirection(float direction)
        {
            if (direction > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (direction < 0)
            {
                _spriteRenderer.flipX = true;
            }
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