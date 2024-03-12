using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace PhysicsProjectileSimulator
{
    public class ProjectileEditor : SceneController
    {
        // references to all the inputsFields, dropdowns and sliders and text components in this scene that need to be handled.
        [SerializeField] private TMP_Dropdown shapeDropdown;
        [SerializeField] private TMP_Dropdown materialDropdown;
        [SerializeField] private TMP_InputField radiusInput;
        [SerializeField] private TMP_InputField lengthInput;
        [SerializeField] private TMP_InputField widthInput;
        [SerializeField] private TMP_InputField heightInput;
        [SerializeField] private Slider restitutionSlider;
        [SerializeField] private Slider frictionCoefficientSlider;
        [SerializeField] private Slider dragSlider;
        [SerializeField] private TMP_Text densityText;
        [SerializeField] private TMP_Text massText;
        [SerializeField] private TMP_Text volumeText;
        [SerializeField] private Material basicMaterial;

        // references to the gameobjects holding the inputs so that they can be switched to active or not active to make them disappear depending on the shape selected
        [SerializeField] private GameObject RadiusObject;
        [SerializeField] private GameObject LengthObject;
        [SerializeField] private GameObject HeightObject;
        [SerializeField] private GameObject WidthObject;

        //reference to the script which restricts what inputs are allowed for the inputfields
        [SerializeField] private InputFieldRestrictor inputFieldRestrictor;


        // all the important variables related to the projectile
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
        private float drag;

        private string selectedString;
        private GameObject projectile1;


        private void Start()
        {
            selectedString = "Sphere";
            material = "Wood";
            radius = 0.1f;
            length = 0.2f;
            height = 0.2f;
            width = 0.2f;
            frictionCoefficient = 0.0f;
            restitution = 0.0f;
            drag = 0.0f;
            //get reference to the projectile gameobject and its Projectile script
            projectile1 = GameObject.FindWithTag("projectile");
            projectile = projectile1.GetComponent<Projectile>();
            projectile.materialName = material;
        }

        private void UpdateProjectile()
        {
            // set density for projectile and also for density variable in this class
            density = projectile.GetDensity();
            // calculate the new volume, mass, scale and update the mesh and rigidbody for the projectile shape depedning on which one is chosen
            if (selectedString == "Sphere")
            {
                volume = projectile.sphereProjectile.CalculateVolume();
                mass = projectile.sphereProjectile.CalculateMass();
                projectile.sphereProjectile.SetMesh();
                projectile.sphereProjectile.SetScale();
                projectile.sphereProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cube")
            {
                volume = projectile.cubeProjectile.CalculateVolume();
                mass = projectile.cubeProjectile.CalculateMass();
                projectile.cubeProjectile.SetMesh();
                projectile.cubeProjectile.SetScale();
                projectile.cubeProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cylinder")
            {
                volume = projectile.cylinderProjectile.CalculateVolume();
                mass = projectile.cylinderProjectile.CalculateMass();
                projectile.cylinderProjectile.SetMesh();
                projectile.cylinderProjectile.SetScale();
                projectile.cylinderProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cone")
            {
                volume = projectile.coneProjectile.CalculateVolume();
                mass = projectile.coneProjectile.CalculateMass();
                projectile.coneProjectile.SetMesh();
                projectile.coneProjectile.SetScale();
                projectile.coneProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Teardrop")
            {
                volume = projectile.teardropProjectile.CalculateVolume();
                mass = projectile.teardropProjectile.CalculateMass();
                projectile.teardropProjectile.SetMesh();
                projectile.teardropProjectile.SetScale();
                projectile.teardropProjectile.UpdateRigidbody();
            }
            // Update the mesh collider by recalculating its bounds and update the physics material variables for drag and mass
            projectile.UpdateMeshesCollidersAndPhyiscsMaterial();
            //update the text for mass and volume as those have had to be recalculated
            massText.text = "Mass: " + RoundToSF(mass, 3) + "kg";
            volumeText.text = "Volume: " + RoundToSF(volume, 3) + "m^3";
        }

        // this function will activate the correct correspondent measurement inputfields so that only the correct ones for that shape are displayed
        // for example if cube is chosen length, width and height inputfields are activated whilst for sphere only radius inputfield is activated
        public void OnShapeDropdownChange()
        {
            // selected string contains the string value of the chosen option in the shape dropdown
            selectedString = shapeDropdown.options[shapeDropdown.value].text;
            if (selectedString == "Sphere")
            {
                SetMeasurementsActive(true, false, false, false);
                projectile.projectileShape = "Sphere";
            }
            else if (selectedString == "Cube")
            {
                SetMeasurementsActive(false, true, true, true);
                projectile.projectileShape = "Cube";
            }
            else if (selectedString == "Cylinder")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Cylinder";
            }
            else if (selectedString == "Cone")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Cone";
            }
            else if (selectedString == "Teardrop")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Teardrop";
            }
            UpdateProjectile();
        }

        // this method changes the material name to that of the chosen option from the material dropdown
        // this way when update projectile is called and density is set inside the projectile class it will be the right one correspondent to the material just selected in the material dropdown
        public void OnMaterialDropdownChanger()
        {
            int selectedIndex = materialDropdown.value;
            material = materialDropdown.options[selectedIndex].text;
            projectile.materialName = material;
            // calling this function updates the projectile class and its variables
            UpdateProjectile();
            densityText.text = "Density: " + density.ToString() + "Kg/m^3";
        }

        // everytime a value is trying to be added to whatever inputfield then it will restrict its possible inputs using the inputFieldRestrictor class
        // this is triggered every time any value is trying to be written in one of the measurement input fields
        public void OnAnyMeasurementInputValueChanged(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnValueChanged();
        }
        
        // everytime you click off or finish editing the measurement inputfield it will trigger the inputfield restrictor OnEndEdit function
        public void OnAnyMeasurementInputEndEdit(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnEndEdit();
        }

        // sets the radius to be applied to projectile 
        //makes sure it is a max of 1m and a minimum of 0.1m
        public void SetRadius()
        {
            radius = Mathf.Max(0.1f, Mathf.Min(1.0f, float.Parse(radiusInput.text)));
            radiusInput.text = radius.ToString();
            projectile.sphereProjectile.radius = radius;
            projectile.cylinderProjectile.radius = radius;
            projectile.coneProjectile.radius = radius;
            UpdateProjectile();
        }

        // sets the length of the cube projectile to what the user has entered
        // however it ensures it is at minimum 0.1m and at max 1m
        public void SetLength()
        {
            length = Mathf.Max(0.1f, Mathf.Min(1.0f, float.Parse(lengthInput.text)));
            lengthInput.text = length.ToString();
            projectile.cubeProjectile.length = length;
            UpdateProjectile();
        }

        // sets the width of the cube projectile to what the user has entered
        // however it ensures it is at minimum 0.1m and at max 1m
        public void SetWidth()
        {
            width = Mathf.Max(0.1f, Mathf.Min(1.0f, float.Parse(widthInput.text)));
            widthInput.text = width.ToString();
            projectile.cubeProjectile.width = width;
            UpdateProjectile();
        }

        // sets the height of the projectile to what the user has entered
        // however it ensures it is at minimum 0.1m and at max 1m
        public void SetHeight()
        {
            height = Mathf.Max(0.1f, Mathf.Min(1.0f, float.Parse(heightInput.text)));
            heightInput.text = height.ToString();
            projectile.sphereProjectile.height = height;
            projectile.cubeProjectile.height = height;
            projectile.cylinderProjectile.height = height;
            projectile.coneProjectile.height = height;
            projectile.teardropProjectile.height = height;
            UpdateProjectile();
        }

        // updates the restitution value of the projectile according to the value on the restitution slider
        public void OnRestitutionSliderChange()
        {
            restitution = restitutionSlider.value;
            projectile.restitution = restitution;
            UpdateProjectile();
        }

        // updates the coefficient of friction value of the projectile according to the value on the coefficient of friction slider
        public void OnCoefficientOfFrictionSliderChange()
        {
            frictionCoefficient = frictionCoefficientSlider.value;
            projectile.frictionalCoefficient = frictionCoefficient;
            UpdateProjectile();
        }

        // updates the drag value of the projectile according to the value on the drag slider
        public void OnDragSliderChange()
        {
            drag = dragSlider.value;
            projectile.drag = drag;
            UpdateProjectile();
        }

        // activates the correct correspondant measurement input fields based on which shape is chosen 
        private void SetMeasurementsActive(bool M1, bool M2, bool M3, bool M4)
        {
            RadiusObject.SetActive(M1);
            LengthObject.SetActive(M2);
            WidthObject.SetActive(M3);
            HeightObject.SetActive(M4);
        }

        // On backToSimulatorButton Click load physics simulator scene
        public void BackToSimulator()
        {
            ChangeScene(2, 1);
        }
    }
}
