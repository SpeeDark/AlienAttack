using System.Collections;
using UnityEngine;

namespace AlienInvironment.Fleet
{
    public class FleetMovement : MonoBehaviour, IMovable, IPauseHandler, IDifficultyAddable
    {
        private Transform _transform;

        [Min(0.1f), SerializeField] private float _speed = 1.2f;
        [Min(0.1f), SerializeField] private float _descentSpeed = 1f;
        [Min(0.01f), SerializeField] private float _lowerDistance = 0.05f;

        private Vector2 _movementVector = Vector2.right;

        private Coroutine FleetLower = null;

        public bool isPaused { get; set; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            if (isPaused)
                return;

            Move();
        }

        public void Move()
        {
            _transform.Translate(_movementVector * _speed * Time.fixedDeltaTime);
        }

        public void SetPauseState(bool isPaused)
        {
            this.isPaused = isPaused;
        }

        public void AddDifficulty(float ratio)
        {
            _speed *= ratio;
            _descentSpeed *= ratio;
            _lowerDistance *= ratio;
        }

        public void ChangeMovementDirection(Vector2 movementVector)
        {
            _movementVector = movementVector;

            FleetLower = StartCoroutine(LowerFleet());
        }

        public void ResetPosition()
        {
            if (FleetLower != null)
                StopCoroutine(FleetLower);

            _transform.position = Vector3.zero;
        }

        private IEnumerator LowerFleet()
        {
            var lowerYPos = _transform.position.y - _lowerDistance;

            float _lerpTime = 0f;

            while (_lerpTime < 1)
            {
                _lerpTime += Time.fixedDeltaTime * _descentSpeed;

                _transform.position = Vector2.Lerp(_transform.position, new Vector2(_transform.position.x, lowerYPos), _lerpTime);

                yield return new WaitForFixedUpdate();
            }
        }

        private void OnEnable() => PauseManager.Instance.Register(this);
        private void OnDisable() => PauseManager.Instance.Unregister(this);
        private void OnDestroy() => PauseManager.Instance.Unregister(this);
    }
}