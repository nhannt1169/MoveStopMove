using UnityEngine;

public class Hair : Item
{
    [SerializeField] private ScriptableObjectHair hairData;

    public override string GetItemName()
    {
        return hairData.GetHairName();
    }

    public override float GetItemPrice()
    {
        return hairData.GetPrice();
    }
}
