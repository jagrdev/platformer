using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionReader : MonoBehaviour
{
    [SerializeField] private Hero character;
    private Vector3 _motion;

    /// <summary>
    /// Перемещает игрока в нужном направлении
    /// </summary>
    /// <param name="context">Содержит информацию о вводе пользователя</param>
    public void OnHorizontalMotion(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<float>();
        _motion = new Vector3(delta, _motion.y);
        character.Move(_motion);
    }
    
    /// <summary>
    /// Перемещает игрока в нужном направлении
    /// </summary>
    /// <param name="context">Содержит информацию о вводе пользователя</param>
    public void OnVerticalMotion(InputAction.CallbackContext context)
    {
        var delta = context.ReadValue<float>();
        _motion = new Vector3( _motion.x, delta);
        character.Move(_motion);
    }
}