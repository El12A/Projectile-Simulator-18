using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereProjectile : projectile
{

    private float radius;
    // Start is called before the first frame update
    void Start()
    {

    }



    public sphereProjectile(string material, float FrictionalCoefficient, float Restitution, float r) : base(material,FrictionalCoefficient,Restitution)
    {
        this.materialName = material;
        density = materials[materialName];
        frictionalCoefficient = FrictionalCoefficient;
        restution = Restitution;
        radius = Mathf.Min(Mathf.Max(0.05f, r), 0.5f);
        Debug.Log(radius);
        CalculateVolume();
        CalculateMass();
    }
    public float CalculateVolume()
    {
        // Volume formula for sphere: v = 4/3 x pi x r^3
        volume = (4 / 3) * Mathf.PI * radius * radius * radius;
        return volume;
    }

    public Vector3 GetSize()
    {
        //sphere model has default of 0.5m radius when scale is all set to 1
        //therefore the object has to scale by the radius (in metres) times 2 to get correct scale for desired radius
        float newScale = 2.0f * ((float)radius);
        return  new Vector3(newScale, newScale, newScale);
    }
}
