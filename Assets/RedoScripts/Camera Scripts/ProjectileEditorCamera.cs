using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class ProjectileEditorCamera : CameraController
    {
        [SerializeField] GameObject projectileEditorCamera;
        // Start is called before the first frame update
        void Start()
        {
            currentCamera = projectileEditorCamera;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

