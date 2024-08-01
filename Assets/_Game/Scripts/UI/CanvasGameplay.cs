using UnityEngine;

public class CanvasGameplay : UICanvas
{
    [SerializeField] Joystick joystick;

    private void Start()
    {
        GameManager.instance.UpdateJoystick(joystick);
    }

    public void SettingsButton()
    {
        UIManager.instance.OpenUI<CanvasSettings>();
    }
}
