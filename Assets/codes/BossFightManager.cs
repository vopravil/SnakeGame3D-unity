using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BossFightManager : MonoBehaviour
{
    public snakeMovement SnakeMovement;
    public snakeShot SnakeShot;
    public GameObject winScreen;
    public GameObject defeatScreen;
    public GameObject darkCave;
    public GameObject goldCave;
    public int fruitsForWin = 6;
    public TMPro.TextMeshProUGUI scoreText;    public TMPro.TextMeshProUGUI winScoreText;
    public TMPro.TextMeshProUGUI GreatEnemyFelled;
    public GameObject minimap;
    public GameObject minimapBackground; 
    public bossCollisions BossCollisions;
    private bool greatEnemyFelledShown = false;
    private bool hasPlayedDefeatSound = false;
    private float time;
    private float bestTime;
    public GameObject pauseScreen;
    GameObject varGameObject;

    void Start()
    {
        varGameObject = GameObject.Find("player");
        darkCave.SetActive(true);
        goldCave.SetActive(false);

        if (winScreen != null) winScreen.SetActive(false);
        if (defeatScreen != null) defeatScreen.SetActive(false);

        SnakeMovement = FindObjectOfType<snakeMovement>();
        SnakeShot = FindObjectOfType<snakeShot>();

               time = 0;
        bestTime = PlayerPrefs.GetFloat("BestTime", -1);       
    }

    void Update()
    {
               time += Time.deltaTime;

        scoreText.text = "Time: " + time.ToString("F2") + "\n" + "Bullets: " + SnakeShot.ballCount + "x";

        if (BossCollisions.isSlain && !greatEnemyFelledShown)        {
            darkCave.SetActive(false);
            goldCave.SetActive(true);
            GreatEnemyFelled.gameObject.SetActive(true);
            greatEnemyFelledShown = true;            StartCoroutine(GreatEnemyFelledText());
            DestroyAllEnemies();
        }

        if (SnakeMovement.collided && !hasPlayedDefeatSound)
        {
            hasPlayedDefeatSound = true;
            StartCoroutine(Defeat());
            FindObjectOfType<audioManager>().Play("Defeat");
            FindObjectOfType<GunCamera>().AfterGameCam();

        }
        if (Input.GetKeyDown(KeyCode.Escape) && SnakeMovement.collided == false)
        {

            varGameObject.GetComponent<snakeShot>().enabled = false;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;

        }
    }

    public void Win()
    {
        FindObjectOfType<GunCamera>().AfterGameCam();
        FindObjectOfType<audioManager>().Play("Win");
        Time.timeScale = 0;
        winScreen.SetActive(true);
        scoreText.gameObject.SetActive(false);

        Debug.Log("Current Time: " + time);
        Debug.Log("Best Time Before Update: " + bestTime);

               if (time < bestTime || bestTime < 0)
        {
            bestTime = time;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Time Saved: " + bestTime);
        }

        winScoreText.text = "Time: " + time.ToString("F2") + " seconds\n" +
                            "Best Time: " + (bestTime < 0 ? "---" : bestTime.ToString("F2") + " seconds");

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
    public IEnumerator GreatEnemyFelledText()
    {
        yield return new WaitForSeconds(1f);
        GreatEnemyFelled.gameObject.SetActive(false);
    }
    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
