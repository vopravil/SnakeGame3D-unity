using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public int nextLevel = 0;

       void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int currentUnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 0);
            if (nextLevel > currentUnlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
                PlayerPrefs.Save();
                Debug.Log("Unlocked new level: " + nextLevel);
            }
        }
    }
}
