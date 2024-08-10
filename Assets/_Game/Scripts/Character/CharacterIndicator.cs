using UnityEngine;

public class CharacterIndicator : MonoBehaviour
{
    private Transform tf;
    private Vector3 initPos;
    [SerializeField] private Character owner;
    private float borderSize = 100f;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.transform;
        initPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(owner.transform.position);
        bool isOffScreen = screenPos.x <= borderSize || screenPos.x >= Screen.width || screenPos.y <= borderSize || screenPos.y >= Screen.height;

        if (isOffScreen)
        {
            if (screenPos.x <= borderSize)
            {
                screenPos.x = borderSize;
            }
            if (screenPos.x >= Screen.width - borderSize)
            {
                screenPos.x = Screen.width - borderSize;
            }
            if (screenPos.y <= borderSize)
            {
                screenPos.y = borderSize;
            }
            if (screenPos.y >= Screen.height - borderSize)
            {
                screenPos.y = Screen.height - borderSize;
            }
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            this.transform.position = new Vector3(worldPos.x, 1f, worldPos.z);
        }
        else
        {
            transform.localPosition = initPos;
        }
        var newV = Camera.main.transform.position - tf.position;
        this.tf.LookAt(tf.position - newV);
    }


}
