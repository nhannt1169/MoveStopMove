using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectHair")]

public class ScriptableObjectHair : ScriptableObject
{
    [SerializeField] private string hairName;
    [SerializeField] private float price;
    [SerializeField] private Texture[] textures;

    public string GetHairName()
    {
        return hairName;
    }
    public float GetPrice()
    {
        return price;
    }
    public Texture[] GetTextures()
    {
        return textures;
    }
    public Texture GetTexture(int index)
    {
        return textures[index];
    }
}
