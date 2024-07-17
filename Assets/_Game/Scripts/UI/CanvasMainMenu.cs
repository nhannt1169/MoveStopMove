using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TMP_Dropdown dropdown;
    public void StartButton()
    {
        Close(0);
        UIManager.instance.OpenUI<CanvasGameplay>();
        LevelManager.instance.StartLevel((Utils.LevelIdx)dropdown.value);
    }

    public void SettingsButton()
    {
        UIManager.instance.OpenUI<CanvasSettings>();
    }

    public void OnDropDownValueChange()
    {

    }
}
