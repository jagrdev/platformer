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
    private Vector3 _motion;

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
}
