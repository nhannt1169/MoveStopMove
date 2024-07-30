using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera uiCam;

    private void Awake()
    {
        instance = this;
    }

    public Camera GetMainCam() { return mainCam; }

    public Camera GetUICam() { return uiCam; }

    public void ChangeMainCamStatus(bool isOn)
    {
        mainCam.gameObject.SetActive(isOn);
    }

    public void ChangeUICamStatus(bool isOn)
    {
        uiCam.gameObject.SetActive(isOn);
    }


}
