using UnityEngine;
using UnityEngine.EventSystems;

public class PanelItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private ButtonItem[] itemButtons;
    private Vector3 panelPosition;
    public float percentThreshold = 0.2f;
    private float difference;
    private void Start()
    {
        panelPosition = transform.position;
        Debug.Log(panelPosition.x + "-" + panelPosition.y + "-" + panelPosition.z);
    }

    public void UpdateAllItemStatus()
    {
        foreach (var button in itemButtons)
        {
            if (button != null)
            {
                button.UpdateItemStatus();
                button.UpdatePrice();
            }
        }
    }

    public void UpdateItemStatus(Utils.ItemStatus status, int idx)
    {
        foreach (var button in itemButtons)
        {
            if (button.GetItemIdx() == idx)
            {
                button.ChangeItemStatus(status);
                break;
            }
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.position = panelPosition;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(panelPosition.x + "-" + panelPosition.y + "-" + panelPosition.z);
        difference = difference + (eventData.pressPosition.x - eventData.position.x) / 1000;
        //Debug.Log(difference);
        transform.position = new Vector3(panelPosition.x - difference, panelPosition.y, panelPosition.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x - eventData.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newPosition = panelPosition;
            if (percentage > 0)
            {
                newPosition += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0)
            {
                newPosition += new Vector3(Screen.width, 0, 0);
            }
            transform.position = newPosition;
            panelPosition = newPosition;
        }
        else
        {
            transform.position = panelPosition;
        }
    }
}
