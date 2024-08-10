using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemIdx;
    public virtual string GetItemName()
    {
        return "";
    }

    public virtual float GetItemPrice()
    {
        return 0;
    }
}
