using TMPro;
using UnityEngine;

public class CanvasDefeat : UICanvas
{
    [SerializeField] TextMeshProUGUI rankText;

    public override void Open()
    {
        base.Open();
        UpdateRankText();
    }

    public void MainMenuButton()
    {
        LevelManager.instance.DestroyCurrLevel();
        UIManager.instance.CloseAllUI();
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }

    public void RestartButton()
    {
        Close(0);
        UIManager.instance.OpenUI<CanvasGameplay>();
        CameraManager.instance.ChangeUICamStatus(false);
        //LevelManager.instance.StartLevel((Utils.LevelIdx)dropdown.value);
        LevelManager.instance.StartLevel(0);
    }

    public void UpdateRankText()
    {
        rankText.text = "#" + (BotManager.instance.GetBotCount() + 1).ToString();
    }
}
