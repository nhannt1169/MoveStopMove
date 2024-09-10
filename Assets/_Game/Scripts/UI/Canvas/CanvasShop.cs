using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] Canvas canvas;
    [SerializeField] CharacterVisual characterVisual;
    private Weapon chosenWeapon = null;
    private Hair chosenHair = null;
    private Pants chosenPants = null;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI errorText;


    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button weaponPanelButton;
    [SerializeField] private Button hairPanelButton;
    [SerializeField] private Button pantsPanelButton;


    [SerializeField] private PanelItem weaponPanel;
    [SerializeField] private PanelItem hairPanel;
    [SerializeField] private PanelItem pantsPanel;

    public override void Open()
    {
        base.Open();
        AssignCamera(CameraManager.instance.GetUICam());
        coinText.text = DataManager.instance.currData.userData.coins + " Coins";
        ChangePanelVisibilityWeapon();
        ResetAllStatus();
    }

    public void ResetAllStatus()
    {
        chosenWeapon = null;
        chosenHair = null;
        chosenPants = null;
        errorText.gameObject.SetActive(false);
        weaponPanel.UpdateAllItemStatus();
        hairPanel.UpdateAllItemStatus();
        pantsPanel.UpdateAllItemStatus();
        buyButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
    }

    private void AssignCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }

    public void ChooseWeapon(int weaponIdx)
    {
        ShowError("");
        characterVisual.SetWeapon(weaponIdx);
        Weapon weapon = ItemManager.instance.weapons[weaponIdx];
        chosenWeapon = weapon;
        chosenHair = null;
        chosenPants = null;
        var currData = DataManager.instance.GetCurrentData();

        buyButton.gameObject.SetActive(!currData.weaponData.weaponData[chosenWeapon.itemIdx].isOwned);
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);

        if (currData.userData.coins < chosenWeapon.GetItemPrice() && buyButton.isActiveAndEnabled)
        {
            ShowError(Utils.errorNotEnoughMoney);
            return;
        }
    }

    public void ChooseHair(int hairIdx)
    {
        ShowError("");
        characterVisual.SetHair(hairIdx);
        Hair hair = ItemManager.instance.hairs[hairIdx];

        chosenHair = hair;
        chosenWeapon = null;
        chosenPants = null;
        var currData = DataManager.instance.GetCurrentData();

        buyButton.gameObject.SetActive(!currData.hairData.hairData[chosenHair.itemIdx].isOwned);
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);

        if (currData.userData.coins < chosenHair.GetItemPrice() && buyButton.isActiveAndEnabled)
        {
            ShowError(Utils.errorNotEnoughMoney);
            return;
        }
    }

    public void ChoosePants(int pantsIdx)
    {
        ShowError("");
        characterVisual.SetPants(pantsIdx);
        Pants pants = ItemManager.instance.pants[pantsIdx];

        chosenPants = pants;
        chosenWeapon = null;
        chosenHair = null;
        var currData = DataManager.instance.GetCurrentData();

        buyButton.gameObject.SetActive(!currData.pantsData.pantsData[chosenPants.itemIdx].isOwned);
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);

        if (currData.userData.coins < chosenPants.GetItemPrice() && buyButton.isActiveAndEnabled)
        {
            ShowError(Utils.errorNotEnoughMoney);
            return;
        }
    }


    public void BuyButton()
    {
        if (chosenWeapon != null)
        {
            var currData = DataManager.instance.GetCurrentData();
            if (currData.userData.coins < chosenWeapon.GetItemPrice())
            {
                ShowError(Utils.errorNotEnoughMoney);
                return;
            }

            currData.userData.coins -= chosenWeapon.GetItemPrice();
            GameManager.instance.GetPlayer().SetWeapon(chosenWeapon.itemIdx);
            var newWeaponData = currData.weaponData;
            newWeaponData.weaponData[chosenWeapon.itemIdx].isOwned = true;
            currData.weaponData.equippedId = chosenWeapon.itemIdx;
            DataManager.instance.SaveToJson();

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            coinText.text = DataManager.instance.currData.userData.coins + " Coins";

            weaponPanel.UpdateAllItemStatus();
        }
        else if (chosenHair != null)
        {
            var currData = DataManager.instance.GetCurrentData();

            if (currData.userData.coins < chosenHair.GetItemPrice())
            {
                ShowError(Utils.errorNotEnoughMoney);
                return;
            }

            currData.userData.coins -= chosenHair.GetItemPrice();
            GameManager.instance.GetPlayer().SetHair(chosenHair.itemIdx);
            var newHairData = currData.hairData;
            newHairData.hairData[chosenHair.itemIdx].isOwned = true;
            currData.hairData.equippedId = chosenHair.itemIdx;
            DataManager.instance.SaveToJson();

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            coinText.text = DataManager.instance.currData.userData.coins + " Coins";
            hairPanel.UpdateAllItemStatus();
        }
        else if (chosenPants != null)
        {
            var currData = DataManager.instance.GetCurrentData();

            if (currData.userData.coins < chosenPants.GetItemPrice())
            {
                ShowError(Utils.errorNotEnoughMoney);
                return;
            }

            currData.userData.coins -= chosenPants.GetItemPrice();
            GameManager.instance.GetPlayer().SetPants(chosenPants.itemIdx);
            var newPantsData = currData.pantsData;
            newPantsData.pantsData[chosenPants.itemIdx].isOwned = true;
            currData.pantsData.equippedId = chosenPants.itemIdx;
            DataManager.instance.SaveToJson();

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            coinText.text = DataManager.instance.currData.userData.coins + " Coins";
            pantsPanel.UpdateAllItemStatus();
        }
    }

    public void EquipButton()
    {
        if (chosenWeapon != null)
        {
            GameManager.instance.GetPlayer().SetWeapon(chosenWeapon.itemIdx);
            DataManager.instance.GetCurrentData().weaponData.equippedId = chosenWeapon.itemIdx;
            DataManager.instance.SaveToJson();
            weaponPanel.UpdateAllItemStatus();
        }
        else if (chosenPants != null)
        {
            GameManager.instance.GetPlayer().SetPants(chosenPants.itemIdx);
            DataManager.instance.GetCurrentData().pantsData.equippedId = chosenPants.itemIdx;
            DataManager.instance.SaveToJson();
            pantsPanel.UpdateAllItemStatus();
        }
        else if (chosenHair != null)
        {
            GameManager.instance.GetPlayer().SetHair(chosenHair.itemIdx);
            DataManager.instance.GetCurrentData().hairData.equippedId = chosenHair.itemIdx;
            DataManager.instance.SaveToJson();
            hairPanel.UpdateAllItemStatus();
        }
    }

    public void BackButton()
    {
        Close(0);
        CameraManager.instance.ChangeUICamStatus(false);
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }

    public void ChangePanelVisibilityWeapon()
    {
        if (!weaponPanel.gameObject.activeSelf)
        {
            weaponPanel.Open();
            hairPanel.Close();
            pantsPanel.Close();
            characterVisual.SetHair(-1);
            characterVisual.SetPants(-1);
            ShowError("");
        }
    }

    public void ChangePanelVisibilityHair()
    {
        if (!hairPanel.gameObject.activeSelf)
        {
            hairPanel.Open();
            weaponPanel.Close();
            pantsPanel.Close();
            characterVisual.SetWeapon(-1);
            characterVisual.SetPants(-1);
            ShowError("");
        }
    }

    public void ChangePanelVisibilityPants()
    {
        if (!pantsPanel.gameObject.activeSelf)
        {
            pantsPanel.Open();
            hairPanel.Close();
            weaponPanel.Close();
            characterVisual.SetWeapon(-1);
            characterVisual.SetHair(-1);
            ShowError("");
        }
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
