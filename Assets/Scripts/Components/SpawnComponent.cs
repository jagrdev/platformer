using UnityEngine;

namespace Components
{
    /// <summary>
    /// Создает нужный объект в указанном месте
    /// </summary>
    public class SpawnComponent : MonoBehaviour
    {
        /// <summary>
        /// Место создания
        /// </summary>
        [SerializeField] private Transform _target;
        
        /// <summary>
        /// Объект для создания
        /// </summary>
        [SerializeField] private GameObject _prefab;
        
        /// <summary>
        /// Вызов создания объекта
        /// </summary>
        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance = Instantiate(_prefab, _target.position, Quaternion.identity);
            instance.transform.localScale = _target.lossyScale;
        }
    }
}
