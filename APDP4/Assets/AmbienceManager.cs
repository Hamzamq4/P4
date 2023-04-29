using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioClip audioClip;
    public float maxLevel = 1.0f;
    public float levelIncrement = 0.01f;

    public AudioSource audioSource;
    public float currentLevel;
    private float timeElapsed = 0.0f;
    public float timeToIncrement = 10f;
    public GameObject player;

    void Start()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
        currentLevel = audioSource.volume;
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
    }
}