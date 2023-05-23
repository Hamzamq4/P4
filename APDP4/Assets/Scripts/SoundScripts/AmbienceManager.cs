using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioClip trafficClip;
    public AudioClip schoolClip;
    public float maxLevel = 1.0f;
    public float levelIncrement = 0.01f;

    public AudioSource audioSource;
    public float currentLevel;
    private float timeElapsed = 0.0f;
    public float timeToIncrement = 10f;

    public GameObject player;



    private int trafficLayer;
    private int schoolLayer;

    public float fadeDuration = 0.5f;
    private bool isFading = false;
    public float fadeTimer = 0.0f;
    private float startVolume = 0.0f;
    private AudioClip nextClip;

    void Start()
    {
        /*audioSource.clip = trafficClip;
        audioSource.loop = true;
        audioSource.Play();
        currentLevel = audioSource.volume;*/

        audioSource.loop = true;
        currentLevel = audioSource.volume;
        trafficLayer = LayerMask.NameToLayer("TrafficTerrain");
        schoolLayer = LayerMask.NameToLayer("SchoolTerrain");
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToIncrement)
        {
            currentLevel += levelIncrement;
            currentLevel = Mathf.Clamp(currentLevel, 0.0f, maxLevel);
            audioSource.volume = currentLevel;
            Debug.Log(currentLevel);
            timeElapsed = 0.0f;
            Debug.Log(audioSource.volume);
        }

        // Check for terrain layers and change audio clip if necessary
       if (player.layer == LayerMask.NameToLayer("TrafficTerrain") && audioSource.clip != trafficClip)
        {
            nextClip = trafficClip;
            StartCoroutine(FadeOutAndIn());
        }
        else if (player.layer == LayerMask.NameToLayer("SchoolTerrain") && audioSource.clip != schoolClip)
        {
            nextClip = schoolClip;
            StartCoroutine(FadeOutAndIn());
        }
    }

    private IEnumerator FadeOutAndIn()
    {
        isFading = true;
        fadeTimer = 0.0f;

        while (fadeTimer < fadeDuration)
        {
            float t = fadeTimer / fadeDuration;
            audioSource.volume = Mathf.Lerp(currentLevel, 0.0f, t);
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = nextClip;
        audioSource.Play();

        while (fadeTimer < fadeDuration * 2)
        {
            float t = (fadeTimer - fadeDuration) / fadeDuration;
            audioSource.volume = Mathf.Lerp(0.0f, currentLevel, t);
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = currentLevel;
        isFading = false;
    }
}    




