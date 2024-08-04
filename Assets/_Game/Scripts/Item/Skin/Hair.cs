using UnityEngine;

public class Hair : MonoBehaviour
{
    [SerializeField] private ScriptableObjectHair hairData;
    public int hairIdx;



    public string GetHairName()
    {
        return hairData.GetHairName();
    }

    public float GetPrice()
    {
        return hairData.GetPrice();
    }
}
