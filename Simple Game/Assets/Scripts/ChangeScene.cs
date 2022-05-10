using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    //Used to load the game part of the application
    public void GameLoader()
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }
    //Used to load the menu part of the application
    public void MenuLoader()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}
