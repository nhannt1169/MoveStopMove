using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float price;

    public string GetWeaponName()
    {
        return weaponName;
    }

    public float GetWeaponPrice()
    {
        return price;
    }
}
