using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public Weapon[] weapons;
    public Throwable[] throwables;
    public Hair[] hairs;
    public Pants[] pants;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].itemIdx = i;
        }

        for (int i = 0; i < hairs.Length; i++)
        {
            hairs[i].itemIdx = i;
        }

        for (int i = 0; i < pants.Length; i++)
        {
            pants[i].itemIdx = i;
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
            data.weaponData.weaponData.Add(new WeaponData(weapons[i].GetItemName(), false));
        }

        for (int i = 0; i < hairs.Length; i++)
        {
            data.hairData.hairData.Add(new HairData(hairs[i].GetItemName(), false));
        }

        for (int i = 0; i < pants.Length; i++)
        {
            data.pantsData.pantsData.Add(new PantsData(pants[i].GetItemName(), false));
        }

        DataManager.instance.SaveToJson();
    }
}
