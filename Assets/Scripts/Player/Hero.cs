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
        public bool IsJumping { get; set; }

        private void Awake()
        {
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

        private void FixedUpdate()
        {
            _isGrounded = layerCheck.IsTouchingLayer;
            var velocity = _rigidbody2D.velocity;
            var x = _motion.x * speed;
            var y = velocity.y;
            if (y > 0 && !IsJumping)
            {
                y /= 3;
            }
            _rigidbody2D.velocity = new Vector2(x, y);
            if (_isGrounded && IsJumping)
            {
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}