using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private ScriptableObjectWeapon weaponData;
    public Utils.PoolType poolType;

    public override string GetItemName()
    {
        return weaponData.GetWeaponName();
    }

    public override float GetItemPrice()
    {
        return weaponData.GetPrice();
    }

    internal Throwable Shoot(Transform attackPos, Character owner, Quaternion rotation)
    {
        if (owner == null)
        {
            return null;
        }
        Throwable throwable = (Throwable)ObjectPool.SpawnObject(attackPos.position, rotation, poolType, null);
        throwable.StartMoving(owner);
        return throwable;
    }
}
