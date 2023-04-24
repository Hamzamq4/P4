using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public GameObject oneLife, twoLives;
    public static int health = 2;

    public GameObject gameOverPanel;
    public int refillLifeTime;

    private Coroutine lifeRefillCoroutine;

    private Animator pAnimator;

    public static bool isPlayerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        health = 2;
        oneLife.SetActive(true);
        twoLives.SetActive(true);

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }

        pAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 2:
                oneLife.SetActive(true);
                twoLives.SetActive(true);
                oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(true);
                break;
            case 1:
                oneLife.SetActive(true);
                twoLives.SetActive(false);
                if (lifeRefillCoroutine == null)
                {
                    // Start a new coroutine to refill the life
                    lifeRefillCoroutine = StartCoroutine(RefillLives());
                }
                break;
            case 0:
                oneLife.SetActive(false);
                twoLives.SetActive(false);
                if (!gameOverPanel.activeSelf)
                {
                    pAnimator.SetTrigger("Death_b");
                    isPlayerAlive = false;
                    StartCoroutine(ShowGameOverPanel());
                }
                oneLife.gameObject.SetActive(false);
                twoLives.gameObject.SetActive(false);
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
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Set the time scale to zero after the delay
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("SampleScene");
        health = 2;
    }
}
