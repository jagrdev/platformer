using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onTakenDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ApplyDamage(float damage)
        {
            _health -= damage;
            _onTakenDamage?.Invoke();
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
    }
}
