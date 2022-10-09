using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadScene : MonoBehaviour
{
    
    public void LoadGameScene() {
        SceneManager.LoadScene("Game");
    }
    public void LoadTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
