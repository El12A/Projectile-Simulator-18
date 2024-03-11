using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    // this scrit will be attached to the projectile camera to ensure that it stays the right rotation and sticks withe the projectile during motion
    public class CameraFollowProjectile : CameraController
    {
        public Transform projectileTransform;
        // to get the camera attached to follow the projectile camera without rotating along with the projectile we have to constantly update its rotatation and location like this
        void Update()
        {
            transform.position = new Vector3(projectileTransform.position.x, projectileTransform.position.y, projectileTransform.position.z - 16);
            transform.rotation = Quaternion.identity;
        }
    }
}

