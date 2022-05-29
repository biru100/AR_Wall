using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene3DManager : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("BlankAR");
        }
    }
}
