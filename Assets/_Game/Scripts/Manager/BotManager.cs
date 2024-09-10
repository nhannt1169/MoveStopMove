using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    List<Bot> bots = new();
    [SerializeField] Bot botPrefab;
    public static BotManager instance;
    public int survivorCount;

    private void Awake()
    {
        instance = this;
    }

    public void InitBots(Transform[] charPositions)
    {
        if (bots.Count > 0)
        {
            DestroyAllBots();
        }
        for (int i = 1; i < charPositions.Length; i++)
        {
            Bot bot = Instantiate(botPrefab, this.transform);
            bot.OnInit(charPositions[i].position);
            bots.Add(bot);
        }

        survivorCount = bots.Count;
    }

    public void RemoveBot(Bot bot)
    {
        if (bots.Contains(bot))
        {
            survivorCount--;
        }
        //bots.Remove(bot);

        //Destroy(bot.gameObject);
    }

    public void DisableAllBots()
    {
        foreach (Bot bot in bots)
        {
            bot.gameObject.SetActive(false);
            Destroy(bot.gameObject);
        }
        bots.Clear();
        //Invoke(nameof(DestroyAllBots), 5f);
    }

    private void DestroyAllBots()
    {
        foreach (Bot bot in bots)
        {
            Destroy(bot.gameObject);
        }

        bots.Clear();
    }

    public int GetBotCount()
    {
        return bots.Count;
    }
}
