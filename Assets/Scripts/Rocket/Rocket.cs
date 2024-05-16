using UnityEngine;

namespace Rocket
{
    public abstract class Rocket : MonoBehaviour
    {
        [SerializeField] private RocketConfig rocketConfig;

        public RocketConfig Config => rocketConfig;
    }
}