using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameChangeScene : MonoBehaviour
{
    public void ChangeScene(int addToIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + addToIndex);
    }
}
