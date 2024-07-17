using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    List<Bot> bots = new();
    [SerializeField] Bot botPrefab;
    public static BotManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void InitBots(Transform[] charPositions)
    {
        for (int i = 1; i < charPositions.Length; i++)
        {
            Bot bot = Instantiate(botPrefab, this.transform);
            bot.OnInit(charPositions[i].position);
            bots.Add(bot);
        }
    }

    public void ClearBots()
    {
        foreach (Bot bot in bots)
        {
            Destroy(bot);
        }
        bots = new();
    }
}
