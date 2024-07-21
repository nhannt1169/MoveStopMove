using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Weapon[] weapons;
    public Throwable[] throwables;

    private void Awake()
    {
        instance = this;
    }
}
