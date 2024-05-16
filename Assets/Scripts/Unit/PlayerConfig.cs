using UnityEngine;

using Rocket;

namespace Unit
{
    [CreateAssetMenu(menuName = "Units/Player", fileName = "PlayerConfig")]
    public class PlayerConfig : UnitConfig
    {
        [Header("Prefabs"), Space]

        [SerializeField] private PlayerRocket playerRocket;

        [Header("PlayerInfo"), Space]

        [SerializeField, Min(0f)] private float _maxHealth;

        [Space(5f)]
        [SerializeField, Min(0.1f)] private float _attackRatePerSecond;

        [Space(5f)]
        [SerializeField, Min(0.01f)] private float _mouseSensitivity;

        private void OnValidate()
        {
            if (_maxHealth < Health)
                _maxHealth = Health;
        }

        public PlayerRocket Rocket => playerRocket;

        public float MaxHealth => _maxHealth;

        public float AttackRate => _attackRatePerSecond;

        public float MouseSensitivity => _mouseSensitivity;
    }
}