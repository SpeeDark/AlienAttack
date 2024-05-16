using UnityEngine;

namespace Unit
{
    public abstract class PlayerUnit : MonoBehaviour, IDamagable
    {
        [SerializeField] private PlayerConfig playerConfig;

        public PlayerConfig Config => playerConfig;

        public abstract void TakeDamage(float value);
    }
}