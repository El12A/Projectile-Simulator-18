using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class CylinderProjectile : Shape
    {
        [SerializeField] private Mesh cylinderMesh;

        public override float CalculateVolume()
        {
            //volume formula for Cylinder: 
            volume = Mathf.PI * height * radius * radius;
            return volume;
        }
        public override void SetScale()
        {
            // cylinder model has default of 0.5m radius, and 1m height so we do this scale to make it accurate in m to what the user enters
            projectile.transform.localScale = new Vector3(radius * 2, height, radius * 2);
        }
        public override float CalculateMass()
        {
            mass = projectile.density * volume;
            return mass;
        }
        public void SetMesh()
        {
            MeshFilter currentMeshFilter = projectileObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = cylinderMesh;
            // rotate the cylinder so it is upright as the 3d mesh spawns tilted
            Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
            projectileObject.transform.localRotation = newRotation;
        }
        public override void UpdateRigidbody()
        {
            projectileRb.mass = mass;
            projectileRb.drag = projectile.drag;
        }
    }
}

