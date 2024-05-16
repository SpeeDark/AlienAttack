using System.Collections;
using UnityEngine;

using Rocket;

namespace Unit
{
    public class AlienShooting : UnitShooting
    {
        private Transform _transform;

        [SerializeField] private Alien alien;

        private const float Minute = 60.0f;

        private float _attackRate;
        private float _attackRateError;

        private AlienRocketsPool alienRocketsPool;

        private void Awake()
        {
            Initialize();

            _transform = GetComponent<Transform>();

            alienRocketsPool = FindObjectOfType<AlienRocketsPool>();
        }

        private void Initialize()
        {
            _attackRate = alien.Config.AttackRate;
            _attackRateError = alien.Config.AttackRateError;
        }

        private void Start()
        {
            DoStartShootingDelay();
        }

        public override void Shoot()
        {
            if (!isPaused)
            {
                AlienRocket Rocket = alienRocketsPool.GetRocket();
                var RocketTransform = Rocket.transform;
                RocketTransform.position = _transform.position;
                RocketTransform.rotation = _transform.rotation;

                Rocket.PlayRocketSound();
            }

            Reload();
        }

        public override void Reload()
        {
            var reloadTime = Minute / _attackRate;
            var reloadTimeError = Random.Range(-reloadTime * _attackRateError, reloadTime * _attackRateError);

            StartCoroutine(Reloading(reloadTime - reloadTimeError));
        }

        private void DoStartShootingDelay()
        {
            var reloadTime = Random.Range(1f, Minute / _attackRate);

            StartCoroutine(Reloading(reloadTime));
        }

        private IEnumerator Reloading(float reloadTime)
        {
            yield return new WaitForSeconds(reloadTime);

            Shoot();
        }
    }
}