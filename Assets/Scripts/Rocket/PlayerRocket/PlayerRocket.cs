using UnityEngine;
using UnityEngine.Events;

namespace Rocket
{
    public class PlayerRocket : Rocket
    {
        public UnityEvent OnHitTarget;
        public UnityEvent OnReachBorder;

        private float _damageValue;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _damageValue = this.Config.Damage;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damageValue);

                OnHitTarget.Invoke();
            }
        }
    }
}