using UnityEngine;

namespace Rocket
{
    public class PlayerRocketMovement : RocketMovement, IDifficultyAddable
    {
        [SerializeField] private PlayerRocket playerRocket;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _transform = GetComponent<Transform>();
            _speed = playerRocket.Config.Speed;
        }

        public void AddDifficulty(float ratio)
        {
            _speed *= ratio;
        }
    }
}