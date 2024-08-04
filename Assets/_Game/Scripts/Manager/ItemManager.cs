using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Weapon[] weapons;
    public Throwable[] throwables;
    public Hair[] hairs;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].weaponIdx = i;
        }

        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].hairIdx = i;
        }

        if (DataManager.instance.isNeededInit)
        {
            InitData(DataManager.instance.currData);
        }
    }

    public void InitData(PlayerData data)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            data.weaponData.weaponData.Add(new WeaponData(weapons[i].GetWeaponName(), false));
        }

        for (int i = 0; i < hairs.Length; i++)
        {
            data.hairData.hairData.Add(new HairData(hairs[i].GetHairName(), false));
        }

        DataManager.instance.SaveToJson();
    }
}
