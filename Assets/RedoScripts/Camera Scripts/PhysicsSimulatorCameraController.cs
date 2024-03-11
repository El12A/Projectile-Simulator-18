using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhysicsProjectileSimulator
{
    public class PhysicsSimulatorCameraController : CameraController
    {
        [SerializeField] private Button buttonNextCameraInQueue;
        [SerializeField] private Button buttonCameraFollowProjectile;
        [SerializeField] private List<GameObject> cameras;

        private CircularQueue<GameObject> cameraQueue;

        private int numLastCamera;
        // Start is called before the first frame update
        void Start()
        {
            projectile = GameObject.FindWithTag("projectile").GetComponent<Projectile>();
            cameraQueue = new CircularQueue<GameObject>(5);
            // populate the camera queue with the camera list given in editor
            foreach (GameObject cam in cameras)
            {
                cameraQueue.Enqueue(cam);
            }
        }

        // when next camera button is clicked switch the main camera to the next camera in queue
        public void OnNextCameraInQueueButtonClick()
        {
            SwitchMainCamera(cameraQueue.GetFrontItem());
        }

        // when follow projectile camera button is clicked then switch main camera to the follow projectile camera
        public void OnFollowProjectileCameraButtonClick()
        {
            numLastCamera = 1;
            SwitchMainCamera(cameraQueue.GetFrontItem());
            numLastCamera = -1;
        }

        public void SwitchMainCamera(GameObject oldMainCamera)
        {
            // if current camera is in camera queue and we want to set projectile camera next as active
            if (numLastCamera == 1)
            {
                // deactivate the current camera shift elements in the queue and activate the projectile camera
                oldMainCamera.SetActive(false);
                cameraQueue.Shift(1);
                projectile.projectileCamera.SetActive(true);
                currentCamera = projectile.projectileCamera;
            }
            // if the main camera currently is projectile camera
            else if (numLastCamera == -1)
            {
                // set projectile camera off and activate the next camera in queue
                projectile.projectileCamera.SetActive(false);
                cameraQueue.GetFrontItem().SetActive(true);
                numLastCamera = 0;
                currentCamera = cameraQueue.GetFrontItem();
            }
            // if last camera was just another camera in the queue
            else
            {
                // set current camera as not active and next camera in queue as active and shift queue
                oldMainCamera.SetActive(false);
                cameraQueue.Shift(1);
                cameraQueue.GetFrontItem().SetActive(true);
                currentCamera = cameraQueue.GetFrontItem();
            }
        }
    }
}

