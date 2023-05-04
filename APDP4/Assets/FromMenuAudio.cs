using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FromMenuAudio : MonoBehaviour
{   
    public AudioClip longIntro;
    public AudioSource radio;
    private bool hasPlayedIntro = false;
    private int activeScene;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
        }
    }
    }
}
