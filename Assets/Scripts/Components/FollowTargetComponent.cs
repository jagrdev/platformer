using UnityEngine;

namespace Components
{
    /// <summary>
    /// Перемещает объект к координатам цели
    /// </summary>
    public class FollowTargetComponent : MonoBehaviour
    {
        /// <summary>
        /// Цель, за которой перемещаем текущий объект
        /// </summary>
        [SerializeField] private Transform target;

        /// <summary>
        /// Парметр сглаживания рывков при перемещении
        /// </summary>
        [SerializeField] private float damping;

        private void LateUpdate()
        {
            var targetPosition = target.position;
            var destination = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * damping);
        }
    }
}
