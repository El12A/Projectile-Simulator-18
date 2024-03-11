using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    [SerializeField] TMP_Text ErrorMessageText;

    public void OnDisplacementChange()
    {
        if (ErrorMessageText.text == "Successfull Input")
        {
            transform.position = GameControl.control.initialPosition + GameControl.control.displacement;
        }
    }
}
