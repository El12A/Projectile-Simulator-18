using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class projectile : MonoBehaviour
{
    protected Dictionary<string, float> materials =
    new Dictionary<string, float>()
    {
        {"Wood", 800},
        {"Polystyrene", 30},
        {"Glass", 2600},
        {"Lead", 11343},
        {"Iron", 7870},
        {"Gold", 19320}
    };
    protected string materialName;
    public float volume;
    public float density;
    public float mass;
    protected bool isStatic;
    public float restution;
    public float frictionalCoefficient;
        
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected projectile(string material, float FrictionalCoefficient, float Restitution)
    {
        this.materialName = material;
        density = materials[materialName];
        frictionalCoefficient = FrictionalCoefficient;
        restution = Restitution;
    }

    protected virtual float CalculateMass()
    {
        mass = density * volume;
        return mass;
    }
}
