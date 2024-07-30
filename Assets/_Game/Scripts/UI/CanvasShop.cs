using TMPro;
using UnityEngine;

public class CanvasShop : UICanvas
{
    [SerializeField] Canvas canvas;
    [SerializeField] CharacterVisual characterVisual;
    private Weapon chosenWeapon = null;
    [SerializeField] private TextMeshProUGUI coinText;

    public override void Open()
    {
        AssignCamera(CameraManager.instance.GetUICam());
    }
    private void AssignCamera(Camera camera)
    {
        canvas.worldCamera = camera;
    }

    public void ChooseWeapon(Weapon weapon)
    {
        characterVisual.SetWeapon(weapon);
        chosenWeapon = weapon;
    }

    public void BuyButton()
    {
    }

    public void BackButton()
    {
        Close(0);
        CameraManager.instance.ChangeUICamStatus(false);
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }
}
