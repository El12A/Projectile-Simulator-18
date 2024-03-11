using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class MainMenuCamera : CameraController
    {
        public GameObject mainMenuCamera;
        // Start is called before the first frame update
        void Start()
        {
            currentCamera = mainMenuCamera;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

