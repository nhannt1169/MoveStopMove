using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectItemStatus")]
public class ScriptableObjectItemStatus : ScriptableObject
{
    [SerializeField] private Texture[] statusTextures;

    internal Texture GetTexture(Utils.ItemStatus status)
    {
        return statusTextures[(int)status];
    }
}
