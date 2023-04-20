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

    private Coroutine lifeRefillCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        health = 2;
        oneLife.gameObject.SetActive(true);
        twoLives.gameObject.SetActive(true);

        if (PlayerPrefs.HasKey ("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (health)
        {
            case 2:
                oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(true);
                Debug.Log("2 lives");
                break;
            case 1:
                oneLife.gameObject.SetActive(true);
                twoLives.gameObject.SetActive(false);
                if (lifeRefillCoroutine == null)
                {
                    // Start a new coroutine to refill the life
                    lifeRefillCoroutine = StartCoroutine(RefillLives());
                }
                Debug.Log("1 life");
                break;
            case 0:
                oneLife.gameObject.SetActive(false);
                twoLives.gameObject.SetActive(false);
                Debug.Log("0 lives");
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
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

    IEnumerator RefillLives()
    {

        yield return new WaitForSeconds(refillLifeTime);
        if(health == 1)
        {
            Debug.Log("2 lives now");
            health += 1;
            lifeRefillCoroutine = null;
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
