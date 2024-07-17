using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
}
