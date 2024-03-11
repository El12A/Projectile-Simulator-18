using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private int shapeNum;
    [SerializeField] private string Material;
    public List<GameObject> spawnShapes = new List<GameObject>();
    private GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnShapes.Count);
        if (shapeNum == 0 && spawnShapes.Count > 0)
        {

        }
        

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public abstract class abstractBody : MonoBehaviour
{
    public double volume;

    public Dictionary<string, double> Materials =
        new Dictionary<string, double>()
        {
                {"Wood", 800},
                {"Polystyrene", 30},
                {"Glass", 2600},
                {"Lead", 11343},
                {"Iron", 7870},
                {"Gold", 19320}

        };
    public string materialName;
    protected double density;
    public double mass;
    public bool isStatic;
    public double Restution;

    public abstractBody(string material)
    {
        this.materialName = material;
        density = Materials[materialName];
        mass = density * volume;
    }

    public virtual double CalculateVolume()
    {
        return volume;
    }
}

public class Sphere : abstractBody
{
    public double radius;
    public GameObject sphereObject;

    public Sphere(string material, double r, GameObject shapeBody) : base(material)
    {
        radius = r;
        CalculateVolume();
        this.materialName = material;
        density = Materials[materialName];
        mass = density * volume;
        sphereObject = shapeBody;
        
    }

    public void Initialize(string material, double r) 
    {
        radius = r;
        CalculateVolume();
        this.materialName = material;
        density = Materials[materialName];
        mass = density * volume;
    }

    public override double CalculateVolume()
    {
        // Volume formula for sphere: v = 4/3 x pi x r^3
        volume = (4 / 3) * Mathf.PI * radius * radius * radius;
        return volume;
    }
    public void SpawnObject()
    {
        Instantiate(sphereObject, transform.position, Quaternion.identity);
    }
}

class Cuboid : abstractBody
{
    public double length;
    public double height;
    public double width;
    public GameObject cubeObject;
    public Cuboid(string material, double length, double height, double width, GameObject shapeBody) : base(material)
    {
        this.length = length;
        this.height = height;
        this.width = width;
        CalculateVolume();
        this.materialName = material;
        density = Materials[materialName];
        mass = density * volume;
        cubeObject = shapeBody;
        SpawnObject();
    }

    public override double CalculateVolume()
    {
        // Volume formula for cuboid: v = l x h x w
        volume = length * height * width;
        return volume;
    }

    public void SpawnObject()
    {
        Instantiate(cubeObject, transform.position, Quaternion.identity);
    }
}

class Cylinder : abstractBody
{
    public double height;
    public double radius;
    public GameObject cylinderObject;
    public Cylinder(string material, double r, double h, GameObject shapeBody) : base(material)
    {
        radius = r;
        height = h;
        CalculateVolume();
        this.materialName = material;
        density = Materials[materialName];
        mass = density * volume;
        cylinderObject = shapeBody;
    }
    public override double CalculateVolume()
    {
        // Volume formula for cylinder: v = pi x r^2 x h
        volume = Mathf.PI * radius * radius * height;
        return volume;
    }
    public void SpawnObject()
    {
        Instantiate(cylinderObject, transform.position, Quaternion.identity);
    }
}


