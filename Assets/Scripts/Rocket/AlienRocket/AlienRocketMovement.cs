using UnityEngine;

namespace Rocket
{
    public class AlienRocketMovement : RocketMovement
    {
        [SerializeField] private AlienRocket alienRocket;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _transform = GetComponent<Transform>();
            _speed = alienRocket.Config.Speed;
        }
    }
}