using UnityEngine;

namespace Rocket
{
    public abstract class RocketMovement : MonoBehaviour, IMovable, IPauseHandler
    {
        protected Transform _transform;

        protected float _speed;

        public bool isPaused { get; set; }

        private void FixedUpdate()
        {
            if (isPaused)
                return;

            Move();
        }

        public void Move()
        {
            _transform.Translate(Vector3.up * _speed * Time.fixedDeltaTime);
        }

        public void SetPauseState(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        private void OnEnable()
        {
            PauseManager.Instance.Register(this);
        }

        private void OnDisable()
        {
            PauseManager.Instance.Unregister(this);
        }

        private void OnDestroy()
        {
            PauseManager.Instance.Unregister(this);
        }
    }
}