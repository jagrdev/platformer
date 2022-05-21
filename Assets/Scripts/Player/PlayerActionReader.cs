using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerActionReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        /// <summary>
        /// Перемещает игрока в нужном направлении
        /// </summary>
        /// <param name="context">Содержит информацию о вводе пользователя</param>
        public void OnMotion(InputAction.CallbackContext context)
        {
            var delta = context.ReadValue<Vector2>();
            _hero.SetMotion(delta);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _hero.Interact();
            }
        }
    }
}