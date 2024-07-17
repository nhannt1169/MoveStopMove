using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Weapon[] weapons;
    public Throwable[] throwables;

    private void Awake()
    {
        instance = this;
        //Load weapons
        /*   Weapon[] weapons = Resources.LoadAll<Weapon>("Weapon/");
           if (weapons.Length == 0)
           {
               Debug.LogError("Cannot load any weapon!");
           }
           for (int i = 0; i < weapons.Length; i++)
           {
               Weapons.Add(i, weapons[i]);
           }*/
    }
}
