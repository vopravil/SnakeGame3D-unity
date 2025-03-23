using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatLoadMenu : MonoBehaviour
{
    public int restartLevel = 0;


    public void LoadMenuScene()
    {
        Time.timeScale = 1;        SceneManager.LoadScene(0);
       
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1;        SceneManager.LoadScene(restartLevel);
       
    }


}
