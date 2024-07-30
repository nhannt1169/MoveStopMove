using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TMP_Dropdown dropdown;
    public void StartButton()
    {
        Close(0);
        UIManager.instance.OpenUI<CanvasGameplay>();
        CameraManager.instance.ChangeUICamStatus(false);
        LevelManager.instance.StartLevel((Utils.LevelIdx)dropdown.value);
    }

    public void ShopButton()
    {
        Close(0);
        CameraManager.instance.ChangeUICamStatus(true);
        UIManager.instance.OpenUI<CanvasShop>();
    }

    public void SettingsButton()
    {
        UIManager.instance.OpenUI<CanvasSettings>();
    }

    public void OnDropDownValueChange()
    {

    }
}
