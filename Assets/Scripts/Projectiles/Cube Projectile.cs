using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeProjectile : projectile
{
    private float length;
    private float width;
    private float height;
    // Start is called before the first frame update
    void Start()
    {

    }

    public CubeProjectile(string material, float FrictionalCoefficient, float Restitution, float l, float w, float h) : base(material, FrictionalCoefficient, Restitution)
    {
        this.materialName = material;
        density = materials[materialName];
        frictionalCoefficient = FrictionalCoefficient;
        restution = Restitution;
        length = Mathf.Min(Mathf.Max(0.05f, l), 1.0f);
        width = Mathf.Min(Mathf.Max(0.05f, w), 1.0f);
        height = Mathf.Min(Mathf.Max(0.05f, h), 1.0f);
        CalculateVolume();
        CalculateMass();
    }
    public float CalculateVolume()
    {
        //volume formula for cube
        volume = length * width * height;
        return volume;
    }

    public Vector3 GetSize()
    {
        return new Vector3(width, height, length);
    }
}
