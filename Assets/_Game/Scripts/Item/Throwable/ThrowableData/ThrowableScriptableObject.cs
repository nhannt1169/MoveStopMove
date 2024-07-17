using UnityEngine;
using static Utils;

[CreateAssetMenu(fileName = "ThrowableScriptableObject")]
public class ThrowableScriptableObject : ScriptableObject
{
    [SerializeField] private WeaponThrowType weaponThrowType;
    [Range(1f, 10f)]
    [SerializeField] private float speed;

    public WeaponThrowType GetWeaponThrowType()
    {
        return weaponThrowType;
    }

    public float GetSpeed()
    {
        return speed;
    }
}
