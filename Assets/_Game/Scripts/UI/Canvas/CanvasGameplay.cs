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

    public void SettingsButton()
    {
        UIManager.instance.OpenUI<CanvasSettings>();
    }

    public void UpdateSurviorCount(int num)
    {
        survivorCountText.text = "Alive: " + num.ToString();
    }
}
