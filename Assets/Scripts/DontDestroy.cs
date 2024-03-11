using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// STOLE CODE from this youtube video https://www.youtube.com/watch?v=HXaFLm3gQws as didn't know how to make effective dont destroy on load script that can be applied to multiple different gameobjects

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
