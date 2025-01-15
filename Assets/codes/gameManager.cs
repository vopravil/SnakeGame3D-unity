using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public snakeShot SnakeShot;
    public GameObject winScreen;
    public GameObject defeatScreen;
    public GameObject darkCave;
    public GameObject goldCave;
    public int fruitsForWin = 6;
    public TMPro.TextMeshProUGUI scoreText; // Correct type for UI text.
    public TMPro.TextMeshProUGUI winScoreText;
    public GameObject minimap;
    public GameObject minimapBackground;

    private float time;
    private float bestTime;

    void Start()
    {
        darkCave.SetActive(true);
        goldCave.SetActive(false);
        if (winScreen != null) winScreen.SetActive(false);
        if (defeatScreen != null) defeatScreen.SetActive(false);

        if (SnakeMovement == null)
        {
            SnakeMovement = FindObjectOfType<snakeMovement>();
        }
        if (SnakeShot == null)
        {
            SnakeShot = FindObjectOfType<snakeShot>();
        }

        // Initialize time variables
        time = 0;
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
    }

    void Update()
    {
        // Increment the timer
        time += Time.deltaTime;

        scoreText.text = "Time: " + time.ToString("F2") + "\n" + "Kills: " + SnakeMovement.kills + "\n" + "Bullets: " + SnakeShot.ballCount + "x" + "\n" + "Fruits: "   + SnakeMovement.fruitCount + " / " + fruitsForWin ;

        if (SnakeMovement.fruitCount >= fruitsForWin)
        {
            darkCave.SetActive(false);
            goldCave.SetActive(true);

         
        }
        if (SnakeMovement.collided == true)
        {
            StartCoroutine(Defeat());
        }
    }

    public void Win()
    {
        Time.timeScale = 0;
        winScreen.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        // Check for best time
        if (time < bestTime)
        {
            bestTime = time;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }

        winScoreText.text = "Time: " + time.ToString("F2") + " seconds" + "\n" +
                            "Best Time: " + (bestTime == float.MaxValue ? "---" : bestTime.ToString("F2") + " seconds") + "\n" +
                            "Kills: " + SnakeMovement.kills + "\n" +
                            "Fruits: " + SnakeMovement.fruitCount;

        minimap.gameObject.SetActive(false);
        minimapBackground.gameObject.SetActive(false);
    }

    public IEnumerator Defeat()
    {
        yield return new WaitForSeconds(2f);
        SnakeMovement.youDiedText.gameObject.SetActive(false);
        Time.timeScale = 0;
        scoreText.gameObject.SetActive(false);
        defeatScreen.gameObject.SetActive(true);
        minimap.gameObject.SetActive(false);
        minimapBackground.gameObject.SetActive(false);
    }

  
}
