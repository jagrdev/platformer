using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onApplyHealth;
        [SerializeField] private UnityEvent _onTakenDamage;
        [SerializeField] private UnityEvent _onDie;

        private float _currentHealth;
        private bool _isInvulnerability;

        private void Awake()
        {
            _currentHealth = _health;
        }

        public void ApplyDamage(float damage)
        {
            Debug.Log($"ApplyDamage = {!_isInvulnerability}");
            if (_isInvulnerability) return;
            _currentHealth -= damage;
            _onTakenDamage?.Invoke();
            if (_currentHealth <= 0)
            {
                _onDie?.Invoke();
            }
        }
        
        public void ApplyHealing(float health)
        {
            _currentHealth += health;
            if (_currentHealth > _health)
            {
                _currentHealth = _health;
            }
            _onApplyHealth?.Invoke();
        }

        /// <summary>
        /// Делает персонажа невоспримчивым к повреждениям
        /// </summary>
        public void SetInvulnerability()
        {
            _isInvulnerability = true;
            Debug.Log($"_isInvulnerability = {_isInvulnerability}");
        }
        
        /// <summary>
        /// Делает персонажа повреждаемым
        /// </summary>
        public void SetVulnerability()
        {
            _isInvulnerability = false;
            Debug.Log($"_isInvulnerability = {_isInvulnerability}");
        }
    }
}
