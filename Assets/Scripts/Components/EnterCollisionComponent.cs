using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    /// <summary>
    /// Обработчик столкновений твердых коллайдеров
    /// </summary>
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _objectTag;
        [SerializeField] private EnterEvent _onCollision;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_objectTag))
            {
                _onCollision?.Invoke(other.gameObject);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_objectTag))
            {
                _onCollision?.Invoke(other.gameObject);
            }
        }

        [Serializable]
        private class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}