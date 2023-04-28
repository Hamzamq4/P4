using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioClip audioClip;
    public float maxLevel = 1.0f;
    public float levelIncrement = 0.1f;

    public AudioSource audioSource;
    private float currentLevel = 1f;
    private float timeElapsed = 0.0f;
    public GameObject player;

    void Start()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 10.0f)
        {
            currentLevel += levelIncrement;
            currentLevel = Mathf.Clamp(currentLevel, 0.0f, maxLevel);
            audioSource.volume = currentLevel;
            Debug.Log(currentLevel);
            timeElapsed = 0.0f;
        }
    }
}