using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    [SerializeField] private RawImage boughtStatusIcon;
    [SerializeField] private ScriptableObjectItemStatus statusData;
    [SerializeField] private Item item;
    [SerializeField] private Utils.ItemType itemType;
    [SerializeField] private CanvasShop canvasShop;
    [SerializeField] private TextMeshProUGUI itemPriceText;

    public int GetItemIdx()
    {
        return item.itemIdx;
    }

    public void ChangeItemStatus(Utils.ItemStatus status)
    {
        boughtStatusIcon.texture = statusData.GetTexture(status);
    }

    public void OnClick()
    {
        switch (itemType)
        {
            case Utils.ItemType.weapon: canvasShop.ChooseWeapon(item.itemIdx); break;
            case Utils.ItemType.hair: canvasShop.ChooseHair(item.itemIdx); break;
            case Utils.ItemType.pants: canvasShop.ChoosePants(item.itemIdx); break;
        }
    }

    public void UpdatePrice()
    {
        itemPriceText.text = item.GetItemPrice().ToString();
    }

    public void UpdateItemStatus()
    {
        Utils.ItemStatus status = Utils.ItemStatus.locked;
        switch (itemType)
        {
            case Utils.ItemType.weapon:
                var weaponData = DataManager.instance.currData.weaponData;
                if (weaponData.weaponData[item.itemIdx].isOwned)
                {
                    if (weaponData.equippedId == item.itemIdx)
                    {
                        status = Utils.ItemStatus.equipped;
                    }
                    else
                    {
                        status = Utils.ItemStatus.unlocked;
                    }
                }
                break;
            case Utils.ItemType.hair:
                var hairData = DataManager.instance.currData.hairData;
                if (hairData.hairData[item.itemIdx].isOwned)
                {
                    if (hairData.equippedId == item.itemIdx)
                    {
                        status = Utils.ItemStatus.equipped;
                    }
                    else
                    {
                        status = Utils.ItemStatus.unlocked;
                    }
                }
                break;
            case Utils.ItemType.pants:
                var pantsData = DataManager.instance.currData.pantsData;
                if (pantsData.pantsData[item.itemIdx].isOwned)
                {
                    if (pantsData.equippedId == item.itemIdx)
                    {
                        status = Utils.ItemStatus.equipped;
                    }
                    else
                    {
                        status = Utils.ItemStatus.unlocked;
                    }
                }
                break;
        }

        ChangeItemStatus(status);
    }
}
