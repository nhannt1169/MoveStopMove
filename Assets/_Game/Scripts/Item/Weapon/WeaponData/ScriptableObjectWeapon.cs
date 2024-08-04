using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectWeapon")]
public class ScriptableObjectWeapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float price;

    public string GetWeaponName()
    {
        return weaponName;
    }

    public float GetPrice()
    {
        return price;
    }
}
