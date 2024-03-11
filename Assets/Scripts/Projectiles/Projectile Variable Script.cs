using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using OpenCover.Framework.Model;
using Assets.Scripts;
// Had to find out how to recalculate the mesh collider based on the new mesh selected by user in the CustomProjectile maker GUI: https://docs.unity3d.com/ScriptReference/Mesh.RecalculateBounds.html
public class ProjectileVariableScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown shapeDropdown;
    [SerializeField] private TMP_Dropdown materialDropdown;
    [SerializeField] private TMP_InputField radiusInput;
    [SerializeField] private TMP_InputField lengthInput;
    [SerializeField] private TMP_InputField widthInput;
    [SerializeField] private TMP_InputField heightInput;
    [SerializeField] private Slider restitutionSlider;
    [SerializeField] private Slider frictionCoefficientSlider;
    [SerializeField] private TMP_Text densityText;
    [SerializeField] private TMP_Text massText;
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private Material basicMaterial;

    [SerializeField] private GameObject RadiusObject;
    [SerializeField] private GameObject LengthObject;
    [SerializeField] private GameObject HeightObject;
    [SerializeField] private GameObject WidthObject;

    [SerializeField] private Mesh sphereMesh;
    [SerializeField] private Mesh cubeMesh;
    [SerializeField] private Mesh cylinderMesh;
    [SerializeField] private Mesh coneMesh;
    [SerializeField] private Mesh teardropMesh;


    private string material;
    private float density;
    private float volume;
    private float mass;
    private float radius;
    private float length;
    private float height;
    private float width;
    private float restitution;
    private float frictionCoefficient;

    private string selectedString;
    private string lastSelectedString;
    private GameObject projectile1;


    private void Start()
    {
        lastSelectedString = GameControl.control.lastSelectedString;
        Debug.Log(lastSelectedString);
        selectedString = "Sphere";
        material = "Wood";
        radius = 0.1f;
        length = 0.2f;
        height = 0.2f;
        width = 0.2f;
        frictionCoefficient = 0.5f;
        restitution = 0.5f;
        projectile1 = GameObject.FindWithTag("projectile");
        
    }

    private void RemoveComponent()
    {
        if (lastSelectedString == "Sphere")
        {
            sphereProjectile oldShape = projectile1.GetComponent<sphereProjectile>();
            Destroy(oldShape);
        }
        else if (lastSelectedString == "Cube")
        {
            CubeProjectile oldShape = projectile1.GetComponent<CubeProjectile>();
            Destroy(oldShape);
        }
        else if (lastSelectedString == "Cylinder")
        {
            CylinderProjectile oldShape = projectile1.GetComponent<CylinderProjectile>();
            Destroy(oldShape);
        }
        else if (lastSelectedString == "Cone")
        {
            ConeProjectile oldShape = projectile1.GetComponent<ConeProjectile>();
            Destroy(oldShape);
        }
        else if (lastSelectedString == "Teardrop")
        {
            TeardropProjectile oldShape = projectile1.GetComponent<TeardropProjectile>();
            Destroy(oldShape);
        }
    }

    private void UpdateProjectile()
    {
        RemoveComponent();
        int selectedIndex = shapeDropdown.value;
        if (selectedString == "Sphere")
        {
            projectile1.GetComponent<MeshFilter>().mesh = sphereMesh;
            UpdateMeshCollider();
            projectile1.AddComponent<sphereProjectile>();
            sphereProjectile sphere = projectile1.GetComponent<sphereProjectile>();
            sphere = new sphereProjectile(material, frictionCoefficient, restitution, radius);
            density = sphere.density;
            mass = sphere.mass;
            volume = sphere.volume;
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
            lastSelectedString = "Sphere";
            projectile1.transform.localScale = sphere.GetSize();
        }
        else if (selectedString == "Cube")
        {
            projectile1.GetComponent<MeshFilter>().mesh = cubeMesh;
            UpdateMeshCollider();
            projectile1.AddComponent<CubeProjectile>();
            CubeProjectile cube = projectile1.GetComponent<CubeProjectile>();
            cube = new CubeProjectile(material, frictionCoefficient, restitution, length, width, height);
            density = cube.density;
            mass = cube.mass;
            volume = cube.volume;
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
            lastSelectedString = "Cube";
            projectile1.transform.localScale = cube.GetSize();
        }
        else if (selectedString == "Cylinder")
        {
            projectile1.GetComponent<MeshFilter>().mesh = cylinderMesh;
            UpdateMeshCollider();
            projectile1.AddComponent<CylinderProjectile>();
            CylinderProjectile cylinder = projectile1.GetComponent<CylinderProjectile>();
            cylinder = new CylinderProjectile(material, frictionCoefficient, restitution, radius, height);
            density = cylinder.density;
            mass = cylinder.mass;
            volume = cylinder.volume;
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
            lastSelectedString = "Cylinder";
            projectile1.transform.localScale = cylinder.GetSize();
            Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
            projectile1.transform.rotation = newRotation;
        }
        else if (selectedString == "Cone")
        {
            projectile1.GetComponent<MeshFilter>().mesh = coneMesh;
            UpdateMeshCollider();
            projectile1.AddComponent<ConeProjectile>();
            ConeProjectile cone = projectile1.GetComponent<ConeProjectile>();
            cone = new ConeProjectile(material, frictionCoefficient, restitution, radius, height);
            density = cone.density;
            mass = cone.mass;
            volume = cone.volume;
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
            lastSelectedString = "Cone";
            projectile1.transform.localScale = cone.GetSize();
            Quaternion newRotation = Quaternion.Euler(-90f, 0f, 0f);
            projectile1.transform.rotation = newRotation; 
        }
        else if (selectedString == "Teardrop")
        {
            projectile1.GetComponent<MeshFilter>().mesh = teardropMesh;
            UpdateMeshCollider();
            projectile1.AddComponent<TeardropProjectile>();
            TeardropProjectile teardrop = projectile1.GetComponent<TeardropProjectile>();
            teardrop = new TeardropProjectile(material, frictionCoefficient, restitution, radius, height);
            density = teardrop.density;
            mass = teardrop.mass;
            volume = teardrop.volume;
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
            lastSelectedString = "Teardrop";
            projectile1.transform.localScale = teardrop.GetSize();
            Quaternion newRotation = Quaternion.Euler(-90f, 0f, 0f);
            projectile1.transform.rotation = newRotation;
        }
        Rigidbody rb = projectile1.GetComponent<Rigidbody>();
        Debug.Log(mass);
        rb.mass = mass;
        Debug.Log(rb.mass);
        Collider collider = projectile1.GetComponent<Collider>();
        PhysicMaterial materialPhysics = collider.material;
        materialPhysics.staticFriction = frictionCoefficient;
        materialPhysics.dynamicFriction = frictionCoefficient;
        materialPhysics.bounciness = restitution;
        GameControl.control.projectile1 = projectile1;
    }

    // retrieves the mesh collider and mesh filter and recalculates the collider based on the meshfilter eg sphere, cube, clinder etc.
    // This is used after there is change in the mesh component of the projectile so that the projectile collisions match the actual shape of the projectile
    private void UpdateMeshCollider()
    {
        MeshFilter projectileMeshFilter = projectile1.GetComponent<MeshFilter>();
        MeshCollider projectileMeshCollider = projectile1.GetComponent<MeshCollider>();
        projectileMeshCollider.sharedMesh = projectileMeshFilter.mesh;
        projectileMeshFilter.mesh.RecalculateBounds();
        projectileMeshCollider.sharedMesh.RecalculateBounds();
    }

    public void OnShapeDropdownChange()
    {
        selectedString = shapeDropdown.options[shapeDropdown.value].text;
        if (selectedString == "Sphere")
        {
            SetMeasurementsActive(true, false, false, false);
            GameControl.control.projectileShape = "Sphere";
        }
        else if (selectedString == "Cube")
        {
            SetMeasurementsActive(false, true, true, true);
            GameControl.control.projectileShape = "Cube";
        }
        else if (selectedString == "Cylinder")
        {
            SetMeasurementsActive(true, false, false, true);
            GameControl.control.projectileShape = "Cylinder";
        }
        else if (selectedString == "Cone")
        {
            SetMeasurementsActive(true, false, false, true);
            GameControl.control.projectileShape = "Cone";
        }
        else if (selectedString == "Teardrop")
        {
            SetMeasurementsActive(true, false, false, true);
            GameControl.control.projectileShape = "Teardrop";
        }
        UpdateProjectile();
    }



    public void OnMaterialDropdownChanger()
    {
        int selectedIndex = materialDropdown.value;
        material = materialDropdown.options[selectedIndex].text;
        // calling this function updates the projectile class and its variables
        UpdateProjectile();
        densityText.text = "Density: " + density.ToString() + "Kg/m^3";
    }

    public void SetRadius()
    {
        radius = float.Parse(radiusInput.text);
        UpdateProjectile();
        Debug.Log(projectile1.transform.transform.localScale);
    }
    public void SetLength()
    {
        length = float.Parse(lengthInput.text);
        UpdateProjectile();
    }
    public void SetWidth()
    {
        width = float.Parse(widthInput.text);
        UpdateProjectile();
    }
    public void SetHeight()
    {
        height = float.Parse(heightInput.text);
        UpdateProjectile();
    }

    public void OnRestitutionSliderChange()
    {
        restitution = restitutionSlider.value;
        UpdateProjectile();
    }

    public void OnCoefficientOfFrictionSliderChange()
    {
        frictionCoefficient = frictionCoefficientSlider.value;
        UpdateProjectile();
    }

    private void SetMeasurementsActive(bool M1, bool M2, bool M3, bool M4)
    {
        RadiusObject.SetActive(M1);
        LengthObject.SetActive(M2);
        WidthObject.SetActive(M3);
        HeightObject.SetActive(M4);
    }

    private string ReturnRoudedString(float num, int sigFigs)
    {
        string numStr = num.ToString();
        if (numStr.Contains('.'))
        {
            // Return the first five characters including the minus sign
            sigFigs++;
        }
        return numStr.Substring(0, Mathf.Min(numStr.Length,sigFigs ));
    }
}
