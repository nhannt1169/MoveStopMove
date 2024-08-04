using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] Canvas canvas;
    [SerializeField] CharacterVisual characterVisual;
    private Weapon chosenWeapon = null;
    private Hair chosenHair = null;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI errorText;


    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button weaponPanelButton;
    [SerializeField] private Button hairPanelButton;

    [SerializeField] private GameObject weaponPanel;
    [SerializeField] private GameObject hairPanel;


    public override void Open()
    {
        base.Open();
        AssignCamera(CameraManager.instance.GetUICam());
        itemNameText.text = "Select an item";
        itemPriceText.text = string.Empty;
        coinText.text = DataManager.instance.currData.userData.coins + " Coins";
        ChangePanelVisibilityWeapon();
        chosenWeapon = null;
        chosenHair = null;
        errorText.gameObject.SetActive(false);
    }
    private void AssignCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }

    public void ChooseWeapon(Weapon weapon)
    {
        characterVisual.SetWeapon(weapon);
        chosenWeapon = weapon;
        itemNameText.text = weapon.GetWeaponName();
        itemPriceText.text = weapon.GetPrice().ToString() + " Coins";
        chosenHair = null;
        var weaponData = DataManager.instance.GetCurrentData().weaponData;

        buyButton.gameObject.SetActive(!weaponData.weaponData[chosenWeapon.weaponIdx].isOwned);
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);
    }

    public void ChooseHair(Hair hair)
    {
        characterVisual.SetHair(hair);
        chosenHair = hair;
        itemNameText.text = hair.GetHairName();
        itemPriceText.text = hair.GetPrice().ToString() + " Coins";
        chosenWeapon = null;
        var hairData = DataManager.instance.GetCurrentData().hairData;

        buyButton.gameObject.SetActive(!hairData.hairData[chosenHair.hairIdx].isOwned);
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);
    }

    public void BuyButton()
    {
        if (chosenWeapon != null)
        {
            var currData = DataManager.instance.GetCurrentData();
            if (currData.userData.coins < chosenWeapon.GetPrice())
            {
                ShowError(Utils.errorNotEnoughMoney);
                return;
            }

            currData.userData.coins -= chosenWeapon.GetPrice();
            GameManager.instance.GetPlayer().SetWeapon(chosenWeapon);
            var newWeaponData = currData.weaponData;
            newWeaponData.weaponData[chosenWeapon.weaponIdx].isOwned = true;
            DataManager.instance.SaveToJson();

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);
            coinText.text = DataManager.instance.currData.userData.coins + " Coins";

        }
        else if (chosenHair != null)
        {
            GameManager.instance.GetPlayer().SetHair(chosenHair);

            var currData = DataManager.instance.GetCurrentData();

            if (currData.userData.coins < chosenHair.GetPrice())
            {
                ShowError(Utils.errorNotEnoughMoney);
                return;
            }

            currData.userData.coins -= chosenHair.GetPrice();
            GameManager.instance.GetPlayer().SetHair(chosenHair);
            var newHairData = currData.hairData;
            newHairData.hairData[chosenHair.hairIdx].isOwned = true;
            DataManager.instance.SaveToJson();

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            coinText.text = DataManager.instance.currData.userData.coins + " Coins";
        }
    }

    public void EquipButton()
    {
        if (chosenWeapon != null)
        {
            GameManager.instance.GetPlayer().SetWeapon(chosenWeapon);
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
        if (!weaponPanel.activeSelf)
        {
            weaponPanel.SetActive(true);
            hairPanel.SetActive(false);
        }
    }

    public void ChangePanelVisibilityHair()
    {
        if (!hairPanel.activeSelf)
        {
            hairPanel.SetActive(true);
            weaponPanel.SetActive(false);
        }
    }

    public void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
