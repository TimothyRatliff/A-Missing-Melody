using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Melody 1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
