using System;
using UnityEngine.Events;

using Rocket;

namespace Unit
{
    public class Alien : AlienUnit
    {
        private float _healthValue;

        public float PrizeForKill => this.Config.PrizeForKill;
        public AlienRocket Rocket => this.Config.Rocket;
        public AlienExplosion Explosion => this.Config.Explosion;

        public UnityEvent OnAlienDeath;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _healthValue = this.Config.Health * GameDifficulty.AlienHealthRatio;
        }

        public void Kill()
        {
            TakeDamage(_healthValue);
        }

        public override void TakeDamage(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException();

            _healthValue -= value;

            if (_healthValue <= 0)
            {
                OnAlienDeath.Invoke();

                Destroy(this.gameObject);
            }
        }
    }
}