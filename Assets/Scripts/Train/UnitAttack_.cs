using System;
using UnityEngine;

public class UnitAttack_ : MonoBehaviour
{
    [SerializeField] private Unit_ unit;

    private float _damage;

    public event Action<float> OnDamageChanged;

    public float Damage
    {
        get => _damage;
        set
        {
            _damage = Mathf.Clamp(value, 0f, float.MaxValue);
            OnDamageChanged?.Invoke(_damage);
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        Damage = unit.Config.Damage;
    }

    public void PerformAttack(UnitDamageable_ recipientUnitDamageable)
    {
        recipientUnitDamageable.ApplyDamage(Damage);
    }
}