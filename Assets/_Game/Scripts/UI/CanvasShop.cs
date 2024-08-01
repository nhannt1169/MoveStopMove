using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShop : UICanvas
{
    [SerializeField] Canvas canvas;
    [SerializeField] CharacterVisual characterVisual;
    private Weapon chosenWeapon = null;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;



    public override void Open()
    {
        AssignCamera(CameraManager.instance.GetUICam());
        itemName.text = string.Empty;
        itemPrice.text = string.Empty;
    }
    private void AssignCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }

    public void ChooseWeapon(Weapon weapon)
    {
        characterVisual.SetWeapon(weapon);
        chosenWeapon = weapon;
        itemName.text = weapon.GetWeaponName();
        itemPrice.text = weapon.GetWeaponPrice().ToString() + " Coins";
        var weaponData = DataManager.instance.GetCurrentData().weaponData;
        switch (chosenWeapon.weaponIdx)
        {
            case 0: buyButton.gameObject.SetActive(!weaponData.weapon_00_bought); break;
            case 1: buyButton.gameObject.SetActive(!weaponData.weapon_01_bought); break;
            case 2: buyButton.gameObject.SetActive(!weaponData.weapon_02_bought); break;
            case 3: buyButton.gameObject.SetActive(!weaponData.weapon_03_bought); break;
        }
        equipButton.gameObject.SetActive(!buyButton.isActiveAndEnabled);
    }

    public void BuyButton()
    {
        if (chosenWeapon != null)
        {
            GameManager.instance.GetPlayer().SetWeapon(chosenWeapon);
            var newWeaponData = DataManager.instance.GetCurrentData().weaponData;
            switch (chosenWeapon.weaponIdx)
            {
                case 0: newWeaponData.weapon_00_bought = true; break;
                case 1: newWeaponData.weapon_01_bought = true; break;
                case 2: newWeaponData.weapon_02_bought = true; break;
                case 3: newWeaponData.weapon_03_bought = true; break;
            }
            buyButton.gameObject.SetActive(false);
            DataManager.instance.SaveToJson();
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
}
