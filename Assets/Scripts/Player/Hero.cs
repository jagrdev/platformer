using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Описывает поведение главного персонажа
/// </summary>
public class Hero : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float jumpForce = 1.0f;

    private Vector3 _motion;
    private Rigidbody2D _rigidbody2D;
    private bool _isJumping;

    public bool IsJumping
    {
        get => _isJumping;
        set => _isJumping = value;
    }

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

    private void Update()
    {
        player.transform.position += _motion * (speed * Time.deltaTime);
    }

    public void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsJumping = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsJumping = true;
    }
}
