using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectColor")]
public class ScriptableObjectColor : ScriptableObject
{
    [SerializeField] private Material[] materials;

    public Material GetMaterial(int idx)
    {
        return materials[idx];
    }

    public Material GetRandomMaterial()
    {
        return GetMaterial(Random.Range(0, materials.Length));
    }
}
