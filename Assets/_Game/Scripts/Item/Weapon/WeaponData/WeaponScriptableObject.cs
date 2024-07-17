using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private float price;
}
