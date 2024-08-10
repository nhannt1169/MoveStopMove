using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool destroyOnClose = false;

    public virtual void Setup()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close(float time)
    {
        Invoke(nameof(CloseForced), time);
    }

    public virtual void CloseForced()
    {
        if (destroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
