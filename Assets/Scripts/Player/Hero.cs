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
    private float _motion = 0f;

    /// <summary>
    /// Перемещение по горизонтали
    /// </summary>
    /// <param name="motion">Отрицательное значение двигает влево, положительное вправо</param>
    public void Move(float motion)
    {
        _motion = motion;
    }

    private void Update()
    {
        var position = player.transform.position;
        player.transform.position = new Vector3(position.x + _motion * speed * Time.deltaTime, position.y);
    }
}
