using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject weaponData;
    public Utils.PoolType poolType;
    public int weaponIdx;

    public string GetWeaponName()
    {
        return weaponData.GetWeaponName();
    }

    public float GetWeaponPrice()
    {
        return weaponData.GetWeaponPrice();
    }

}
