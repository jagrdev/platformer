using UnityEngine;

namespace Player
{
    /// <summary>
    /// Применяет указанный урон по объекту
    /// </summary>
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private float _damage;

        public void ApplyDamage(GameObject gameObject)
        {
            var healthComponent = gameObject.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }
        }
    }
}
