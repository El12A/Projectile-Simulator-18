using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProjectileCameraButton : MonoBehaviour
{
    [SerializeField] private Button buttonCamera;
    [SerializeField] private Camera projectileCamera;
    // Start is called before the first frame update
    void Start()
    {
        buttonCamera.onClick.AddListener(SetMainCamera);
    }

    private void SetMainCamera()
    {
        Debug.Log("new mian camera");
        // Set the specified camera as the main camera
        if (projectileCamera != null)
        {
            Camera.main.enabled = false; // Disable the current main camera
            projectileCamera.enabled = true; // Enable the new main camera

        }
        else
        {
            Debug.LogWarning("Camera to set as main is not assigned!");
        }
    }
}
