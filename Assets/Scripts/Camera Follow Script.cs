using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform projectileTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (projectileTransform.position.x, projectileTransform.position.y, projectileTransform.position.z - 16);
        transform.rotation = Quaternion.identity;
    }
}
