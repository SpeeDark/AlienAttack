using UnityEngine;

namespace Unit
{
    public class UnitConfig : ScriptableObject
    {
        [Header("Common")]

        [SerializeField] private float _health;

        public float Health => _health;
    }
}