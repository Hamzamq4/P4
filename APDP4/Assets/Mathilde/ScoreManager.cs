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
    public TMP_Text highScore;

    public float scoreCount;
    public float highScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public GameObject oneLife, twoLives;
    public static int health;

    public GameObject gameOverPanel;
    public int refillLifeTime;

<<<<<<< Updated upstream
    private Coroutine lifeRefillCoroutine;
=======
    private Animator pAnimator;

    public static bool isPlayerAlive = true;
>>>>>>> Stashed changes


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        health = 2;
        oneLife.gameObject.SetActive(true);
        twoLives.gameObject.SetActive(true);

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }

        pAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    {
        switch (health)
        {
            case 2:
                oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(true);
                break;
            case 1:
                oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(false);
<<<<<<< Updated upstream
                if (lifeRefillCoroutine == null)
                {
                    // Start a new coroutine to refill the life
                    lifeRefillCoroutine = StartCoroutine(RefillLives());
                }
                Debug.Log("1 life");
=======
                StartCoroutine(RefillLives());
>>>>>>> Stashed changes
                break;
            case 0:
                oneLife.gameObject.SetActive(false);
                twoLives.gameObject.SetActive(false);
                if (!gameOverPanel.activeSelf)
                {
                    pAnimator.SetTrigger("Death_b");
                    isPlayerAlive = false;
                    StartCoroutine(ShowGameOverPanel());
                }
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
        highScore.text = "High Score: " + Mathf.Round(highScoreCount).ToString();
    }

<<<<<<< Updated upstream
    IEnumerator RefillLives()
    {

        yield return new WaitForSeconds(refillLifeTime);
        if(health == 1)
        {
            Debug.Log("2 lives now");
            health += 1;
            lifeRefillCoroutine = null;
        }
=======

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator RefillLives()
    {
        yield return new WaitForSeconds(refillLifeTime);
        health = 2;
        Health();
>>>>>>> Stashed changes
    }

    void Health()
    {

    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
