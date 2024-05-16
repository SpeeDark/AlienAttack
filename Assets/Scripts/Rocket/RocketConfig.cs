using UnityEngine;

namespace Rocket
{
    [CreateAssetMenu(menuName = "Rocket", fileName = "RocketConfig")]
    public class RocketConfig : ScriptableObject
    {
        [Header("Common")]

        [SerializeField, Min(0)] private float _damageValue;
        [SerializeField, Min(0)] private float _speed;

        public float Damage => _damageValue;
        public float Speed => _speed;
    }
}