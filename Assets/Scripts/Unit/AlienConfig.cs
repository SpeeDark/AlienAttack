using UnityEngine;

using Rocket;

namespace Unit
{
    [CreateAssetMenu(menuName = "Units/Alien", fileName = "AlienConfig")]
    public class AlienConfig : UnitConfig
    {
        [Header("Prefabs"), Space]

        [SerializeField] private AlienExplosion alienExplosion;
        [SerializeField] private AlienRocket alienRocket;

        [Header("AlienInfo"), Space]

        [SerializeField, Min(0f)] private float _prizeForKill;

        [Header("Shooting"), Space]

        [SerializeField, Min(0.1f)] private float _attackRatePerMinute;
        [Tooltip("Reload time error. 0 - no error. 1 - attack rate error = attack rate per minute")]
        [SerializeField, Range(0f, 1f)] private float _attackRateError;

        public AlienExplosion Explosion => alienExplosion;
        public AlienRocket Rocket => alienRocket;

        public float PrizeForKill => _prizeForKill;

        public float AttackRate => _attackRatePerMinute;
        public float AttackRateError => _attackRateError;
    }
}