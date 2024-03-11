using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CustomDataStructures;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Button buttonChangeCamera;
    [SerializeField] private Button buttonCameraFollowProjectile;
    [SerializeField] private List<GameObject> cameras;

    private CircularQueue<GameObject> cameraQueue;
    private GameObject projectileCamera;

    private int numLastCamera;
    // Start is called before the first frame update
    void Start()
    {
        projectileCamera = GameControl.control.projectileCamera;
        cameraQueue = new CircularQueue<GameObject> (5);
        foreach (GameObject cam in cameras)
        {
            cameraQueue.Enqueue(cam);
        }
        GameControl.control.currentCamera = cameraQueue.GetFrontItem();
    }

    public void NextCameraInQueue()
    {
        Debug.Log("numlast camera" +  numLastCamera);
        SwitchMainCamera(cameraQueue.GetFrontItem());
    }

    public void SetProjectileCameraAsMain()
    {
        numLastCamera = 1;
        SwitchMainCamera(cameraQueue.GetFrontItem());
        numLastCamera = -1;
    }

    public void SwitchMainCamera(GameObject oldMainCamera)
    {
        if (numLastCamera == 1)
        {
            oldMainCamera.SetActive(false);
            cameraQueue.Shift(1);
            projectileCamera.SetActive(true);
            GameControl.control.currentCamera = projectileCamera;
        }
        else if (numLastCamera == -1)
        {
            projectileCamera.SetActive(false);
            cameraQueue.GetFrontItem().SetActive(true);
            numLastCamera = 0;
            GameControl.control.currentCamera = cameraQueue.GetFrontItem();
        }
        else
        {
            oldMainCamera.SetActive(false);
            cameraQueue.Shift(1);
            cameraQueue.GetFrontItem().SetActive(true);
            GameControl.control.currentCamera = cameraQueue.GetFrontItem();
        }
    }
}
