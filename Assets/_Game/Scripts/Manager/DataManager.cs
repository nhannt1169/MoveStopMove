using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public static DataManager instance;
    string filePath = Application.persistentDataPath + "/save.json";
    public PlayerData currData;
    public bool isNeededInit = false;
    public void OnInit()
    {
        if (!LoadFromJson())
        {
            currData = new PlayerData(new PlayerDataWeapon(), new PlayerDataHair(), new PlayerDataPants(), new PlayerDataUser());
            isNeededInit = true;
        }
        instance = this;
    }
    public void SaveToJson()
    {
        currData ??= new PlayerData(new PlayerDataWeapon(), new PlayerDataHair(), new PlayerDataPants(), new PlayerDataUser());
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
        catch (FileNotFoundException e)
        {
            Debug.LogWarning("Error: " + e.Message);
            return false;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
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
    public PlayerDataHair hairData;
    public PlayerDataPants pantsData;
    public PlayerDataUser userData;

    public PlayerData(PlayerDataWeapon weaponData, PlayerDataHair hairData, PlayerDataPants pantsData, PlayerDataUser userData)
    {
        this.weaponData = weaponData;
        this.hairData = hairData;
        this.pantsData = pantsData;
        this.userData = userData;
    }
}

[Serializable]
public class PlayerDataWeapon
{
    public List<WeaponData> weaponData = new();
    public int equippedId = -1;
}

[Serializable]
public class WeaponData
{
    public string name;
    public bool isOwned;
    public WeaponData(string name, bool isOwned)
    {
        this.name = name;
        this.isOwned = isOwned;
    }
}

[Serializable]
public class PlayerDataHair
{
    public List<HairData> hairData = new();
    public int equippedId = -1;
}

[Serializable]
public class HairData
{
    public string name;
    public bool isOwned;
    public HairData(string name, bool isOwned)
    {
        this.name = name;
        this.isOwned = isOwned;
    }
}

[Serializable]
public class PlayerDataPants
{
    public List<PantsData> pantsData = new();
    public int equippedId = -1;
}

[Serializable]
public class PantsData
{
    public string name;
    public bool isOwned;
    public PantsData(string name, bool isOwned)
    {
        this.name = name;
        this.isOwned = isOwned;
    }
}

[Serializable]
public class PlayerDataUser
{
    public float coins = 0;
}

