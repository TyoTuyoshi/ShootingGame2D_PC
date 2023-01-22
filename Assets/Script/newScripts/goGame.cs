using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goGame : MonoBehaviour
{
    public void goGameScene()
    {
        SceneManager.LoadScene("GAMESCENE2");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
