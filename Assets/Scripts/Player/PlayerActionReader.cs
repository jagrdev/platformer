using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionReader : MonoBehaviour
{
    [SerializeField] private Hero player;

    /// <summary>
    /// Перемещает игрока в нужном направлении
    /// </summary>
    /// <param name="context">Содержит информацию о вводе пользователя</param>
    public void OnPlayerMotion(InputAction.CallbackContext context)
    {
        var motion = context.ReadValue<float>();
        player.Move(motion);
    }
}