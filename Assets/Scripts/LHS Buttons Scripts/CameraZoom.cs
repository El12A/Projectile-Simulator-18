using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Button zoomInButton;
    [SerializeField] private Button zoomOutButton;
    private Camera targetCamera;
    private float zoomStep = 5f;
    private float minZoom = 10f;
    private float maxZoom = 50f;

    private void Start()
    {

    }

    public void ZoomIn()
    {
        targetCamera = GameControl.control.currentCamera.GetComponent<Camera>();
        AdjustZoom(-zoomStep);
    }

    public void ZoomOut()
    {
        targetCamera = GameControl.control.currentCamera.GetComponent<Camera>();
        AdjustZoom(zoomStep);
    }

    private void AdjustZoom(float amount)
    {
        float newZoom = Mathf.Clamp(targetCamera.fieldOfView + amount, minZoom, maxZoom);
        SetZoom(newZoom);
    }

    private void SetZoom(float zoom)
    {
        targetCamera.fieldOfView = zoom;
    }
}
