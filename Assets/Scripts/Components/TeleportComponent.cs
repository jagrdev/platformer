using UnityEngine;

namespace Components
{
    /// <summary>
    /// Перемещает объект в заданные координаты
    /// </summary>
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        
        public void Teleport(GameObject target)
        {
            target.transform.position = _destination.position;
        }
    }
}
