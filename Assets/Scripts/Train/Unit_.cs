using UnityEngine;

public abstract class Unit_ : MonoBehaviour
{
    [SerializeField] private UnitConfig_ unitConfig;
    public UnitConfig_ Config => unitConfig;
}