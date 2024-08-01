using System;
using UnityEngine;

public class DataManager
{
    public static DataManager instance;
    string filePath = Application.persistentDataPath + "/save.json";
    public PlayerData currData;
    public void OnInit()
    {
        if (!LoadFromJson())
        {
            currData = new PlayerData(new PlayerDataWeapon(), new PlayerDataUser());
        }
        instance = this;
    }
    public void SaveToJson()
    {
        currData ??= new PlayerData(new PlayerDataWeapon(), new PlayerDataUser());
        Debug.Log(filePath);
        string data = JsonUtility.ToJson(currData);
        System.IO.File.WriteAllText(filePath, data);
    }

    public bool LoadFromJson()
    {
        try
        {
            string data = System.IO.File.ReadAllText(filePath);
            currData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error: " + e.Message);
            return false;
        }
    }

    public PlayerData GetCurrentData()
    {
        return currData;
    }
}

[Serializable]
public class PlayerData
{
    public PlayerDataWeapon weaponData;
    public PlayerDataUser userData;

    public PlayerData(PlayerDataWeapon saveDataWeapon, PlayerDataUser saveDataUser)
    {
        this.weaponData = saveDataWeapon;
        this.userData = saveDataUser;
    }
}

[Serializable]
public class PlayerDataWeapon
{
    public bool weapon_00_bought = false;
    public bool weapon_01_bought = false;
    public bool weapon_02_bought = false;
    public bool weapon_03_bought = false;
}

[Serializable]
public class PlayerDataUser
{
    public int coins = 0;
}

