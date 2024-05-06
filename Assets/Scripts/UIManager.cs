using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI angleXText;
    public TextMeshProUGUI angleYText;
    public TextMeshProUGUI angleZText;
    public GameObject infoPanel;
    private GameObject capsule1;
    private GameObject capsule2;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        capsule1 = GameController.Instance.capsule1;
        capsule2 = GameController.Instance.capsule2;
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Update the position of the UI panel
        infoPanel.transform.position = capsule1.transform.position + new Vector3(0, 2.5f, 0);
        // Make sure that the info panel is always facing the camera
        if (mainCamera != null)
        {
            infoPanel.transform.forward = mainCamera.transform.forward;
        }

        // Update the content of the UI panel
        GetDistance();
        distanceText.text = GetDistance().ToString("F1");
        GetRotation();
        angleXText.text = GetRotation().x.ToString("F1");
        angleYText.text = GetRotation().y.ToString("F1");
        angleZText.text = GetRotation().z.ToString("F1");
    }

    float GetDistance()
    {
        Vector3 direction = capsule2.transform.position - capsule1.transform.position;
        return direction.magnitude;
    }

    Vector3 GetRotation()
    {
        Vector3 euler1 = capsule1.transform.rotation.eulerAngles;
        Vector3 euler2 = capsule2.transform.rotation.eulerAngles;
        float angleX = Mathf.DeltaAngle(euler1.x, euler2.x);
        float angleY = Mathf.DeltaAngle(euler1.y, euler2.y);
        float angleZ = Mathf.DeltaAngle(euler1.z, euler2.z);
        return new Vector3(angleX, angleY, angleZ);
    }
}
