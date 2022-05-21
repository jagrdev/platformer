using UnityEngine;

namespace Player
{
    /// <summary>
    /// Применяет указанный урон по объекту
    /// </summary>
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _health;

        public void ApplyDamage(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_damage);
            }
        }
        
        public void ApplyHealing(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyHealing(_health);
            }
        }
    }
}
