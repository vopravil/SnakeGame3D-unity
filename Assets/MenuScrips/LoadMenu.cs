using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public int nextlevel = 0;
    GameObject varGameObject;

    public void LoadMenuScene()
    {
        Time.timeScale = 1;        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("UnlockedLevel", nextlevel);
        PlayerPrefs.Save();
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;        SceneManager.LoadScene(nextlevel);
        PlayerPrefs.SetInt("UnlockedLevel", nextlevel);
        PlayerPrefs.Save();
    }
    public void Resume()
    {
        varGameObject = GameObject.Find("player");
        Time.timeScale = 1;
        varGameObject.GetComponent<snakeShot>().enabled = true;
    }
    public void LoadMenuFromPauseMenu()
    {
        Time.timeScale = 1;        SceneManager.LoadScene(1);
        
    }


}
