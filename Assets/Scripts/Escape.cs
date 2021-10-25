using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "ModesMenu")
            {
                SceneManager.LoadScene("StartMenu");
            }
            else if (SceneManager.GetActiveScene().name == "StartMenu")
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("ModesMenu");
            }
        }
    }
}
