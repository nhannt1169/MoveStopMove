public class CanvasSettings : UICanvas
{
    public void MainMenuButton()
    {
        LevelManager.instance.DestroyCurrLevel();
        UIManager.instance.CloseAllUI();
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }

    public void ResumeButton()
    {
        Close(0);
    }
}
