using UnityEngine;

namespace Player
{
    /// <summary>
    /// Проверяет, что есть пересечение с нужным слоем
    /// </summary>
    public class LayerCheck : MonoBehaviour
    {
        /// <summary>
        /// Слой, с которым проверяется соприкосновение
        /// </summary>
        [SerializeField] private LayerMask checkingLayer;
        /// <summary>
        /// Коллайдер объекта, который должен пересекаться со слоем
        /// </summary>
        private Collider2D _collider;
    
        /// <summary>
        /// Признак пересечения
        /// </summary>
        private bool _isTouchingLayer; 
        public bool IsTouchingLayer => _isTouchingLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(checkingLayer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(checkingLayer);
        }
    }
}
