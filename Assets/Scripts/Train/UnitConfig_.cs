using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/Config", fileName = "UnitConfig", order = 0)]
public sealed class UnitConfig_ : ScriptableObject
{
    [Header("Name")]

    [SerializeField] private string unitName;

    [Header("Common"), Space]

    [SerializeField, Min(0)] private float health;
    [SerializeField, Min(0)] private float damage;
    [SerializeField, Min(0)] private float speed;

    public string Name => unitName;

    public float Health => health;
    public float Damage => damage;
    public float Speed => speed;
}