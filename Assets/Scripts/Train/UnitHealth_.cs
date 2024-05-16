using System;
using UnityEngine;

public class UnitHealth_ : MonoBehaviour
{
    [SerializeField] private Unit_ unit;

    private float _health;

    public event Action<float> OnHealthChanged;

    public float MaxHealth { get; private set; }

    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0f, MaxHealth);
            OnHealthChanged?.Invoke(_health);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        MaxHealth = unit.Config.Health;
        Health = MaxHealth;
    }
}