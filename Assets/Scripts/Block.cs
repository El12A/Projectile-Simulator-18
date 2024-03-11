using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
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
    private double density;
    public double mass;
    public bool isStatic;
    public double Restution;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
