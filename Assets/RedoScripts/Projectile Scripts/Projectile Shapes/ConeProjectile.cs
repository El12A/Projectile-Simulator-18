using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class ConeProjectile : Shape
    {
        [SerializeField] private Mesh coneMesh;

        public override float CalculateVolume()
        {
            //volume formula for cone: 
            volume = (height / 3) * Mathf.PI * radius * radius;
            return volume;
        }
        public override void SetScale()
        {
            // cone model has default of 0.01m radius, height 0.02m, so we do this scale to make it accurate in m to what the user enters
            float newScale = 2.0f * radius;
            projectile.transform.localScale = new Vector3(radius * 100f, radius * 100f, height * 50f);
        }
        public override float CalculateMass()
        {
            mass = projectile.density * volume;
            return mass;
        }
        public void SetMesh()
        {
            MeshFilter currentMeshFilter = projectileObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = coneMesh;
            // cone mesh is spawned rotated incorrectly so need to rotate to correct orientation
            Quaternion newRotation = Quaternion.Euler(-90f, 0f, 0f);
            projectile.transform.localRotation = newRotation;
        }
        public override void UpdateRigidbody()
        {
            projectileRb.mass = mass;
            projectileRb.drag = projectile.drag;
        }
    }
}

