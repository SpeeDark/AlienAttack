using UnityEngine;

namespace Unit
{
    public abstract class AlienUnit : MonoBehaviour, IDamagable
    {
        [SerializeField] private AlienConfig alienConfig;

        public AlienConfig Config => alienConfig;

        public abstract void TakeDamage(float value);
    }
}