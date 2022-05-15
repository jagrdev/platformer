﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerActionReader : MonoBehaviour
    {
        [SerializeField] private Hero character;

        /// <summary>
        /// Перемещает игрока в нужном направлении
        /// </summary>
        /// <param name="context">Содержит информацию о вводе пользователя</param>
        public void OnMotion(InputAction.CallbackContext context)
        {
            var delta = context.ReadValue<Vector2>();
            character.SetMotion(delta);
        }
    }
}