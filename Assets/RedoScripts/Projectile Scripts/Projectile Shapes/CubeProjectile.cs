using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhysicsProjectileSimulator
{
    public class CubeProjectile : Shape
    {
        [SerializeField] private Mesh cubeMesh;

        public override float CalculateVolume()
        {
            //volume formula for cube
            volume = length * width * height;
            return volume;
        }
        public override void SetScale()
        {
            projectile.transform.localScale = new Vector3(width, height, length);
        }
        public override float CalculateMass()
        {
            mass = projectile.density * volume;
            return mass;
        }
        public void SetMesh()
        {
            MeshFilter currentMeshFilter = projectileObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = cubeMesh;
        }
        public override void UpdateRigidbody()
        {
            projectileRb.mass = mass;
            projectileRb.drag = projectile.drag;
        }
    }
}

