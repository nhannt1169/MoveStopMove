using System.Collections.Generic;
using UnityEngine;


public class ObjectPool
{
    private static Dictionary<Utils.PoolType, Pool> pools = new();

    public static void CreatePool(GameUnit prefab, int amount, Utils.PoolType poolType, Transform parent)
    {
        if (!pools.ContainsKey(poolType) || pools[poolType] == null)
        {
            Pool pool = new();
            pool.CreatePool(prefab, amount, parent);
            pools[poolType] = pool;
        }
    }

    public static GameUnit SpawnObject(Vector3 pos, Quaternion rot, Utils.PoolType poolType, Transform parent)
    {
        if (!PoolExist(poolType))
        {
            return null;
        }
        return pools[poolType].SpawnObject(pos, rot, false, parent);
    }

    public static void DespawnObject(GameUnit unit, Utils.PoolType poolType)
    {
        if (!PoolExist(poolType))
        {
            return;
        }
        pools[poolType].DespawnObject(unit);
    }

    public static void DestroyPool(Utils.PoolType poolType)
    {
        if (!PoolExist(poolType))
        {
            return;
        }
        pools[poolType].DestroyPool();
        pools.Remove(poolType);
    }

    public static bool PoolExist(Utils.PoolType poolType)
    {
        return (pools.ContainsKey(poolType) && pools[poolType] != null);
    }
}

public class Pool
{
    List<GameUnit> actives = new();
    List<GameUnit> inactives = new();
    GameUnit prefab;

    public void CreatePool(GameUnit prefab, int amount, Transform parent = null)
    {
        this.prefab = prefab;
        for (int i = 0; i < amount; i++)
        {
            DespawnObject(SpawnObject(prefab.TF.position, Quaternion.identity, true, parent));
        }
    }

    public GameUnit SpawnObject(Vector3 pos, Quaternion rot, bool forceCreate, Transform parent)
    {
        GameUnit unit;
        if (inactives.Count == 0 || forceCreate)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives[^1];
            unit.gameObject.SetActive(true);
            inactives.Remove(unit);
        }

        unit.TF.parent = parent;
        unit.TF.SetLocalPositionAndRotation(pos, rot);


        actives.Add(unit);

        return unit;
    }

    public void DespawnObject(GameUnit unit)
    {
        if (unit != null)
        {
            actives.Remove(unit);
            inactives.Add(unit);
            unit.gameObject.SetActive(false);
        }
    }

    public void DestroyObject(GameUnit gameObject)
    {
        if (actives.Count > 0)
        {
            if (actives.Contains(gameObject))
            {
                actives.Remove(gameObject);
            }
        }

        if (inactives.Count > 0)
        {
            if (inactives.Contains(gameObject))
            {
                inactives.Remove(gameObject);
            }
        }
        GameObject.Destroy(gameObject);
    }

    public void DestroyPool()
    {
        foreach (GameUnit unit in actives)
        {
            GameObject.Destroy(unit.gameObject);
        }
        foreach (GameUnit unit in inactives)
        {
            GameObject.Destroy(unit.gameObject);
        }
        actives.Clear();
        inactives.Clear();
    }
}
