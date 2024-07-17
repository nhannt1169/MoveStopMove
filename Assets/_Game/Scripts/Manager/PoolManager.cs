using System;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] PreAllocation[] preAllocations;
    public static PoolManager instance;
    public bool PoolCreated { get; private set; }

    private void Awake()
    {
        instance = this;
        PoolCreated = false;
    }
    public void CreatePools()
    {
        for (int i = 0; i < preAllocations.Length; i++)
        {
            ObjectPool.CreatePool(preAllocations[i].unit, preAllocations[i].amount, preAllocations[i].poolType, this.gameObject.transform);
        }
        PoolCreated = true;
    }

    public void DestroyAllPools()
    {
        foreach (Utils.PoolType pool in Enum.GetValues(typeof(Utils.PoolType)))
        {
            ObjectPool.DestroyPool(pool);
        }
        PoolCreated = false;
    }
}

[System.Serializable]
public class PreAllocation
{
    public GameUnit unit;
    public int amount;
    public Utils.PoolType poolType;
}
