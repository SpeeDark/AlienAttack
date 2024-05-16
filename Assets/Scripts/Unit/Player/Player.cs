using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unit
{
    public class Player : PlayerUnit
    {
        private AudioSource hitSound;

        private float _health;
        private float _maxHealth;

        //private int _playerShipsLeft = 1;

        public float Health => _health;
        public float MaxHealth => _maxHealth;

        public UnityEvent OnHealthChanged;
        //public UnityEvent OnPlayerDeath;
        public UnityEvent OnPlayerShipsOver;

        public void Treat(float value)
        {
            _health += value;

            OnHealthChanged.Invoke();

            if (_health > _maxHealth)
                _health = _maxHealth;
        }

        private void Awake()
        {
            Initialize();

            hitSound = GetComponent<AudioSource>();
        }

        private void Initialize()
        {
            _health = this.Config.Health;
            _maxHealth = this.Config.MaxHealth;
        }

        public void PlayerDead()
        {
            _health = 0f;
            //_playerShipsLeft -= 1;

            OnHealthChanged.Invoke();

            //if (_playerShipsLeft <= 0)
            OnPlayerShipsOver.Invoke();
            //else
            //OnPlayerDeath.Invoke();
        }

        public override void TakeDamage(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();

            _health -= value;

            if (_health <= 0f)
            {
                PlayerDead();
                return;
            }

            OnHealthChanged.Invoke();

            hitSound.PlayOneShot(hitSound.clip);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Alien alien))
            {
                alien.Kill();
                PlayerDead();
            }
        }
    }
}