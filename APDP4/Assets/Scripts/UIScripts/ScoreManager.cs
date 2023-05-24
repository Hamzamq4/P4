using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

/// <summary>
/// This script gradually increases the player score, as well as checking the health of the player,
/// and increasing the player health when the player has lost a health. 
/// Upon the player losing all health, an enemy prefab spawns, followed by the game overpanel showing. 
/// </summary>

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

    void Update()
    {
        switch (health)
        {
            case 2:
                zeroLives.SetActive(false);
                oneLife.SetActive(false);
                twoLives.SetActive(true);
                break;
                
            case 1:
                zeroLives.SetActive(false);
                oneLife.SetActive(true);
                twoLives.SetActive(false);
                if (lifeRefillCoroutine == null)
                {
                    // starts a new coroutine to refill health
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
                    isPlayerAlive = false;
                    StartCoroutine(ShowGameOverPanel());
                }
                zeroLives.gameObject.SetActive(false);
                break;
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
        // refill the life only if the player has not lost another life in the meantime
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
            hasSpawned = true; // set the hasSpawned variable to true to prevent it from spawning more than once

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 spawnPosition = player.transform.position - new Vector3(0f, 0f, 20f); // spawns the prefab 20 units behind the player
            GameObject spawnedObject = Instantiate(enemyDeathSpawnPrefab, spawnPosition, Quaternion.identity);
            Vector3 direction = (player.transform.position - spawnPosition).normalized; // gets the direction from spawn position to the player's position
            spawnedObject.transform.rotation = Quaternion.LookRotation(direction); // rotates the spawned object to face the player
            while (Vector3.Distance(spawnedObject.transform.position, player.transform.position) > 10f) // move the spawned object towards the player across 10 units

            {
                spawnedObject.transform.position += direction * Time.deltaTime * 5f; // speed 5f, change if required.
                yield return null;
            }
            radio.PlayOneShot(fangetClip);
            Debug.Log("DeathAnimationTime");
        }
        yield return new WaitForSeconds(3f); // wait for 3 seconds
        gameOverPanel.SetActive(true);
        scoreUI.gameObject.SetActive(false);
        Time.timeScale = 0; // set the time scale to zero after WaitForSeconds
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("SampleScene");
        scoreUI.gameObject.SetActive(true);
        health = 2;
        ScoreManager.isPlayerAlive = true;
    }
}