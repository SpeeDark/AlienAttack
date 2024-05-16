using System.Collections;
using UnityEngine;

using Rocket;

namespace Unit
{
    public class PlayerShooting : UnitShooting, IDifficultyAddable
    {
        private Transform _transform;

        [SerializeField] private Player player;
        [SerializeField] private PlayerRocketsPool playerRocketsPool;
        [SerializeField] private AudioClip shotSound;

        private float _attackRate;

        private bool _canShoot = true;

        private void Awake()
        {
            Initialize();

            _transform = GetComponent<Transform>();
        }

        private void Initialize()
        {
            isPaused = false;

            _attackRate = player.Config.AttackRate;
        }

        private void Update()
        {
            if (isPaused)
                return;

            if (Input.GetMouseButton(0) & _canShoot)
                Shoot();
        }

        public void AddDifficulty(float ratio)
        {
            _attackRate *= ratio;
        }

        public override void Shoot()
        {
            _canShoot = false;

            PlayerRocket Rocket = playerRocketsPool.GetRocket();
            Rocket.transform.position = _transform.position;

            AudioSource.PlayClipAtPoint(shotSound, _transform.position);

            Reload();
        }

        public override void Reload() => StartCoroutine(Reloading());

        private IEnumerator Reloading()
        {
            yield return new WaitForSeconds(1f / _attackRate);
            _canShoot = true;
        }
    }
}