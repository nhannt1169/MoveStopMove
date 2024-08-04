using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ScriptableObjectWeapon weaponData;
    public Utils.PoolType poolType;
    public int weaponIdx;

    public string GetWeaponName()
    {
        return weaponData.GetWeaponName();
    }

    public float GetPrice()
    {
        return weaponData.GetPrice();
    }

    internal Throwable Shoot(Transform attackPos, Transform target, Character owner, Quaternion rotation)
    {
        Throwable throwable = (Throwable)ObjectPool.SpawnObject(attackPos.position, rotation, poolType, null);
        throwable.StartMoving(target, owner);
        return throwable;
    }
}
