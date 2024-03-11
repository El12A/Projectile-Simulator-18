using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsProjectileSimulator
{
    public class SphereProjectile : Shape
    {
        // this is set in the editor and is a mesh of a sphere 3d object
        [SerializeField] private Mesh sphereMesh;

        public override float CalculateVolume()
        {
            // Volume formula for sphere: v = 4/3 x pi x r^3
            volume = (4 / 3) * Mathf.PI * radius * radius * radius;
            return volume;
        }
        public override void SetScale()
        {
            //sphere model has default of 0.5m radius when scale is all set to 1
            //therefore the object has to scale by the radius (in metres) times 2 to get correct scale for desired radius
            float newScale = 2.0f * radius;
            projectileObject.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        // this is a simple equation of density * volume = mass 
        public override float CalculateMass()
        {
            mass = projectile.density * volume;
            return mass;
        }
        // This sets the mesh component of the gameobject to be the sphereMesh specifically
        public void SetMesh()
        {
            MeshFilter currentMeshFilter = projectileObject.GetComponent<MeshFilter>();
            currentMeshFilter.mesh = sphereMesh;
        }
        // updates the variables of the rigidbody component so that its mass and drag is that which the user has entered.
        public override void UpdateRigidbody()
        {
            projectileRb.mass = mass;
            projectileRb.drag = projectile.drag;
        }
    }
}
