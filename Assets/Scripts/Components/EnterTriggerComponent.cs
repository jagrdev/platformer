using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    /// <summary>
    /// Обработчик коллайдеров в режиме триггера
    /// </summary>
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string objectTag;
        [SerializeField] private EnterEvent action;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(objectTag))
            {
                action?.Invoke(other.gameObject);
            }
        }

        [Serializable]
        private class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}
