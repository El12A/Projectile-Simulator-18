using PhysicsProjectileSimulator;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class InputHeight : MonoBehaviour
{
    [SerializeField] private GameObject projectileShooter;
    public InputFieldRestrictor inputFieldRestrictor;

    public void OnInputHeightEndEdit()
    {
        inputFieldRestrictor.OnEndEdit();
        float initialHeight = float.Parse(inputFieldRestrictor.inputField.text);
        Vector3 pro = projectileShooter.transform.position;
        // set new height of the projectile shooter gameobject based on input
        projectileShooter.transform.position = new Vector3(pro.x, pro.y + initialHeight - 1, pro.z);
    }
}
