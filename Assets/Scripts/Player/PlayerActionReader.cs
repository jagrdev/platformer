using UnityEngine;
using UnityEngine.InputSystem;

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
        character.Move(delta);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !character.IsJumping)
        {
            character.Jump();
        }
    }
}