using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform[] charPositions;
    public void OnInit()
    {
    }

    public void DestoyCurrLevel()
    {
        Destroy(gameObject);
    }
}
