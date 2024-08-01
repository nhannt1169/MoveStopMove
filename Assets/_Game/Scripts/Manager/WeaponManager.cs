using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Weapon[] weapons;
    public Throwable[] throwables;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].weaponIdx = i;
        }
    }
}
