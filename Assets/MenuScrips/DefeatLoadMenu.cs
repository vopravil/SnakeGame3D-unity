using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatLoadMenu : MonoBehaviour
{
    public int restartLevel = 0;


    public void LoadMenuScene()
    {
        Time.timeScale = 1; // Ensure the game is unpaused
        SceneManager.LoadScene(1);
       
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Ensure the game is unpaused
        SceneManager.LoadScene(restartLevel);
       
    }


}
