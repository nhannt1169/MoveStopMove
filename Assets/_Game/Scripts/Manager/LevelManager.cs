using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Player player;
    private Dictionary<LevelIdx, Level> levels = new();


    Level currLevel;
    //[SerializeField] NavMeshSurface navMeshSurface;

    private void Awake()
    {
        instance = this;

        //Load levels
        Level[] levels = Resources.LoadAll<Level>("Level/");
        for (int i = 0; i < levels.Length; i++)
        {
            this.levels.Add((LevelIdx)i, levels[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.OpenUI<CanvasMainMenu>();
    }

    public void StartLevel(LevelIdx level)
    {
        //Pool
        if (PoolManager.instance.PoolCreated)
        {
            PoolManager.instance.DestroyAllPools();
        }

        PoolManager.instance.CreatePools();

        //Create level
        currLevel = Instantiate(levels[level]);
        currLevel.OnInit();

        //Create player and bots

        player.OnInit(currLevel.charPositions[0].position);

        BotManager.instance.InitBots(currLevel.charPositions);
    }

    public void DestroyCurrLevel()
    {
        player.ResetStatus();
        player.gameObject.SetActive(false);
        DestroyBots();

        PoolManager.instance.DestroyAllPools();

        currLevel.DestoyCurrLevel();
    }

    public void OnWin(Vector3 position)
    {
        DestroyBots();
        player.Win(position);
    }

    public void DestroyBots()
    {
        //foreach (Bot bot in bots)
        //{
        //    bot.ResetStatus();
        //    bot.gameObject.SetActive(false);
        //}
    }

    public void UpdateJoystick(Joystick joystick)
    {
        player.SetJoystick(joystick);
    }
}
