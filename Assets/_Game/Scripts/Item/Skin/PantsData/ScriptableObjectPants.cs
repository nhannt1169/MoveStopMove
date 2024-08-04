using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectPants")]

public class ScriptableObjectPants : ScriptableObject
{
    [SerializeField] private string pantsName;
    [SerializeField] private float price;
    [SerializeField] private Texture texture;

    public string GetPantsName()
    {
        return pantsName;
    }
    public float GetPrice()
    {
        return price;
    }
    public Texture GetTexture()
    {
        return texture;
    }
}
