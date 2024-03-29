using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
using System;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public TMP_Text highScoreText;

    public GameObject scoreUI;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    private string scoreFilePath;

    public GameObject zeroLives, oneLife, twoLives;
    public static int health = 2;

    public GameObject gameOverPanel;
    public int refillLifeTime;

    private Coroutine lifeRefillCoroutine;

    private Animator pAnimator;

    public static bool isPlayerAlive = true;


    public GameObject enemyDeathSpawnPrefab;
    private bool hasSpawned = false;

    public AudioClip fangetClip;

    public bool saveScore = false;

    private AudioSource radio;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }

        Time.timeScale = 1;

        health = 2;
        zeroLives.SetActive(false);
        oneLife.SetActive(false);
        twoLives.SetActive(false);
        GameObject radioObject = GameObject.FindGameObjectWithTag("Radio");
        radio = radioObject.GetComponent<AudioSource>();

        pAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
       

    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(highScoreCount);
        Debug.Log("File path: " + scoreFilePath);
        Debug.Log("High score text: " + highScoreText.text);*/
        
        switch (health)
        {
            case 2:
                zeroLives.SetActive(false);
                oneLife.SetActive(false);
                twoLives.SetActive(true);
                /* oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(true); */
                break;
                
            case 1:
                zeroLives.SetActive(false);
                oneLife.SetActive(true);
                twoLives.SetActive(false);
                if (lifeRefillCoroutine == null)
                {
                    // Start a new coroutine to refill the life
                    lifeRefillCoroutine = StartCoroutine(RefillLives());
                }
                break;

            case 0:
                zeroLives.SetActive(true);
                oneLife.SetActive(false);
                twoLives.SetActive(false);
                if (!gameOverPanel.activeSelf)
                {
                    scoreIncreasing = false;
                    //pAnimator.SetTrigger("Death_b");
                    isPlayerAlive = false;
                    StartCoroutine(ShowGameOverPanel());
                }
                zeroLives.gameObject.SetActive(false);
                /*oneLife.gameObject.SetActive(false);
                twoLives.gameObject.SetActive(false); */
                break;

                // Check if the player is alive
        }


        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        endScoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount).ToString();

    }


    
    IEnumerator RefillLives()
    {
        yield return new WaitForSeconds(refillLifeTime);
        // Refill the life only if the player has not lost another life in the meantime
        if (health == 1)
        {
            Debug.Log("2 lives now");
            health += 1;
            lifeRefillCoroutine = null;
        }
    }

    IEnumerator ShowGameOverPanel()
    {
        if (!hasSpawned)
        {
            hasSpawned = true; // Set the hasSpawned variable to true to prevent spawning more than once

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 spawnPosition = player.transform.position - new Vector3(0f, 0f, 20f); // Spawn the prefab 20 units behind the player
            GameObject spawnedObject = Instantiate(enemyDeathSpawnPrefab, spawnPosition, Quaternion.identity);
            // Get the direction from the spawn position to the player's position
            Vector3 direction = (player.transform.position - spawnPosition).normalized;

            // Set the initial rotation of the spawned object to face the player
            spawnedObject.transform.rotation = Quaternion.LookRotation(direction);

            // Move the spawned object towards the player across 10 units
            while (Vector3.Distance(spawnedObject.transform.position, player.transform.position) > 10f)
            {
                spawnedObject.transform.position += direction * Time.deltaTime * 5f; // Change the speed here as required
                yield return null;
            }
            radio.PlayOneShot(fangetClip);
            Debug.Log("DeathAnimationTime");
        }
        yield return new WaitForSeconds(3f); // wait for 3 seconds
        gameOverPanel.SetActive(true);
        scoreUI.gameObject.SetActive(false);
        Time.timeScale = 0; // Set the time scale to zero after the delay
    }

    public void ReloadGame()
    {
        //pAnimator.ResetTrigger("Death_b");
        SceneManager.LoadScene("SampleScene");
        scoreUI.gameObject.SetActive(true);
        health = 2;
        //pAnimator.SetTrigger("Static_b");
        ScoreManager.isPlayerAlive = true;

    }

public class DataSaver
    {
    public Array[] allScores;
    }

}