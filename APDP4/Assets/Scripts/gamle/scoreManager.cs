using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScore;

    public float scoreCount;
    public float hiScoreCount;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey ("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime; 

        }

        if(scoreCount > hiScoreCount) 
        {
            hiScoreCount = scoreCount; 
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScore.text = "High Score: " + Mathf.Round(hiScoreCount).ToString(); 

    }
}
