using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Camera mainCamera;
    private Transform mainCameraTransform;
    void Start()
    {
        mainCamera= Camera.main;
        mainCameraTransform = mainCamera.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCameraTransform);
    }
}
