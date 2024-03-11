using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhysicsProjectileSimulator
{
    // this is used to handle the current selected camera this will vary in scenes as well as when changing sceness
    public class CameraController : GameComponent
    {
        public GameObject currentCamera;
        public Camera targetCamera;

        public PhysicsSimulatorCameraController physicsSimulatorCameraController;
        private float zoomStep = 5f;
        private float maxZoom = 10f;
        private float minZoom = 50f;


        public void ZoomIn()
        {
            targetCamera = currentCamera.GetComponent<Camera>();
            AdjustZoom(-zoomStep);
        }

        public void ZoomOut()
        {
            targetCamera = currentCamera.GetComponent<Camera>();
            AdjustZoom(zoomStep);
        }

        private void AdjustZoom(float amount)
        {
            float newZoom = Mathf.Clamp(targetCamera.fieldOfView + amount, maxZoom, minZoom);
            SetZoom(newZoom);
        }

        private void SetZoom(float zoom)
        {
            targetCamera.fieldOfView = zoom;
        }
    }
}

