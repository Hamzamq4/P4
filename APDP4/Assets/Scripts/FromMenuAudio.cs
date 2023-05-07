using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromMenuAudio : MonoBehaviour
{   
    public AudioClip longIntro;
    public AudioSource radio;
    public GameObject gameManager;
    private bool hasPlayedIntro = false;
    private int activeScene;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

    }

    // Update is called once per frame
    void Update()
    {
    activeScene = SceneManager.GetActiveScene().buildIndex;
    if(!hasPlayedIntro)
    {
        if(activeScene == 1)
        {
        GameObject radioObject = GameObject.FindGameObjectWithTag("Radio");
        radio = radioObject.GetComponent<AudioSource>();
        radio.clip = longIntro;
        radio.Play();
        hasPlayedIntro = true;

        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.GetComponent<ObstacleGenerator>().spawnStartTime = 15;
        }
    }
    }
}
