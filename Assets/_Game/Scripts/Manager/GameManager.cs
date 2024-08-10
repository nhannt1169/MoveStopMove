using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Player player;

    private void Awake()
    {
        instance = this;
        DataManager dataManager = new();
        dataManager.OnInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void InitPlayer()
    {
        var currData = DataManager.instance.currData;
        player.SetWeapon(currData.weaponData.equippedId);
        player.SetHair(currData.hairData.equippedId);
        player.SetPants(currData.pantsData.equippedId);
    }

    public void UpdateJoystick(Joystick joystick)
    {
        player.SetJoystick(joystick);
    }
}
