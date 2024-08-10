using UnityEngine;

public class Pants : Item
{
    [SerializeField] private ScriptableObjectPants pantsData;
    [SerializeField] private Texture texture;

    public override string GetItemName()
    {
        return pantsData.GetPantsName();
    }

    public override float GetItemPrice()
    {
        return pantsData.GetPrice();
    }

    public Texture GetTexture()
    {
        return texture;
    }

}
