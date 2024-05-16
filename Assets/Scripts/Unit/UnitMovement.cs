using UnityEngine;

namespace Unit
{
    public abstract class UnitMovement : MonoBehaviour, IMovable, IPauseHandler
    {
        public bool isPaused { get; set; }

        public abstract void Move();

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