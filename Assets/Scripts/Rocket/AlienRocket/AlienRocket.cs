using UnityEngine;
using UnityEngine.Events;

namespace Rocket
{
    public class AlienRocket : Rocket
    {
        private AudioSource _rocketSound;

        private float _damageValue;

        public UnityEvent OnHit;

        private void Awake()
        {
            _rocketSound = GetComponent<AudioSource>();

            Initialize();
        }

        private void Initialize()
        {
            _damageValue = this.Config.Damage;
        }

        public void PlayRocketSound()
        {
            _rocketSound.Play();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damageValue);

                OnHit.Invoke();

                gameObject.SetActive(false);
            }
        }
    }
}