using System.Collections.Generic;
using UnityEngine;
using static Utils;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
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

        GameManager.instance.GetPlayer().OnInit(currLevel.charPositions[0].position);

        BotManager.instance.InitBots(currLevel.charPositions);
    }

    public void DestroyCurrLevel()
    {
        GameManager.instance.GetPlayer().ResetStatus();
        GameManager.instance.GetPlayer().gameObject.SetActive(false);

        PoolManager.instance.DestroyAllPools();

        BotManager.instance.ClearBots();

        currLevel.DestoyCurrLevel();
    }

    public void OnWin()
    {
        BotManager.instance.ClearBots();
        //GameManager.instance.GetPlayer().Win(position);
    }

    public void OnLose()
    {
        BotManager.instance.ClearBots();
        UIManager.instance.CloseAllUI();
        UIManager.instance.OpenUI<CanvasDefeat>();
    }
}
