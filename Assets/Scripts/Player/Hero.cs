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

        private Vector3 _motion;
        private Rigidbody2D _rigidbody2D;

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
        }

        private void ShowMoneyInThePocket()
        {
            var points = _pocket.SilverCoins + _pocket.GoldenCoins * 10;
            Debug.Log($"All points: {points}\n" +
                      $"Silver: {_pocket.SilverCoins}\n" +
                      $"Golden: {_pocket.GoldenCoins}");
        }
    }
}