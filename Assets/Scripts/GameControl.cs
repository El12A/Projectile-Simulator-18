using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=J6FfcJpbPXE&t=589s

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    // In game Variable variables
    public List<string> selectedVariables = new List<string>(3);
    public List<string> unselectedVariables = new List<string>(2);
    public string allSelectedVariableNames;
    public Vector3 initialVelocity = Vector3.zero;
    public Vector3 finalVelocity = Vector3.zero;
    public Vector3 displacement = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 time = Vector3.zero;
    [SerializeField] public GameObject projectileCamera;
    //Projectile variables
    public sphereProjectile projectileSphere;
    public CubeProjectile projectileCube;
    public CylinderProjectile projectileCylinder;
    public ConeProjectile projectileCone;
    public TeardropProjectile projectileTeardrop;
    public GameObject projectile1;
    public string projectileShape;
    public string lastSelectedString = "Sphere"; 
    public Vector3 initialPosition;
    public Vector3 veryInitialPosition;


    public List<GameObject> spawnInputsPrefabList;
    public GameObject currentCamera;
    

    // script variables
    public FindMissingVariables findMissingVariables;
    public TextController textController;

    private void Start()
    {

    }

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        foreach (string var in selectedVariables)
        {
            allSelectedVariableNames = allSelectedVariableNames + var;
        }
    }

    public void OnGUI()
    {

    }

}

