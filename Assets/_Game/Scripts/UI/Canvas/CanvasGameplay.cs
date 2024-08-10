using TMPro;
using UnityEngine;

public class CanvasGameplay : UICanvas
{
    [SerializeField] Joystick joystick;
    [SerializeField] TextMeshProUGUI survivorCountText;

    private void Start()
    {
        GameManager.instance.UpdateJoystick(joystick);
    }

    private void Update()
    {
        UpdateSurviorCount();
    }

    public void SettingsButton()
    {
        UIManager.instance.OpenUI<CanvasSettings>();
    }

    public void UpdateSurviorCount()
    {
        survivorCountText.text = "Surviors: " + (BotManager.instance.GetBotCount() + 1).ToString();
    }
}
