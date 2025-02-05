using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public snakeShot SnakeShot;
    public GameObject winScreen;
    public GameObject defeatScreen;
    public GameObject pauseScreen;
    public GameObject darkCave;
    public GameObject goldCave;
    public int fruitsForWin = 6;
    public TMPro.TextMeshProUGUI scoreText;    public TMPro.TextMeshProUGUI winScoreText;
    public GameObject minimap;
    public GameObject minimapBackground;
    GameObject varGameObject; 
    private float time;
    private float bestTime;
    private bool hasPlayedDefeatSound = false;
    void Start()
    {
        varGameObject = GameObject.Find("player");
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

               time = 0;
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SnakeMovement.collided == false)
        {

            varGameObject.GetComponent<snakeShot>().enabled = false;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            
        }
               time += Time.deltaTime;

        scoreText.text = "Time: " + time.ToString("F2") + "\n" + "Kills: " + SnakeMovement.kills + "\n" + "Bullets: " + SnakeShot.ballCount + "x" + "\n" + "Fruits: " + SnakeMovement.fruitCount + " / " + fruitsForWin;

        if (SnakeMovement.fruitCount >= fruitsForWin)
        {
            darkCave.SetActive(false);
            goldCave.SetActive(true);
        }

        if (SnakeMovement.collided == true && !hasPlayedDefeatSound)
        {
            hasPlayedDefeatSound = true;            StartCoroutine(Defeat());
            FindObjectOfType<audioManager>().Play("Defeat");
            FindObjectOfType<GunCamera>().AfterGameCam();
        }
    }

    public void Win()
    {
        FindObjectOfType<GunCamera>().AfterGameCam();
        FindObjectOfType<audioManager>().Play("Win");
        Time.timeScale = 0;
        winScreen.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

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
