using System;
using UnityEngine;

public class UnitDamageable_ : MonoBehaviour
{
    [SerializeField] private UnitHealth_ unitHealth;

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage));

        var totalDamage = ProcessDamage(damage);

        if (totalDamage < 0)
            throw new ArgumentOutOfRangeException(nameof(totalDamage));

        unitHealth.Health -= totalDamage;
    }

    protected virtual float ProcessDamage(float damage)
    {
        return damage;
    }
}