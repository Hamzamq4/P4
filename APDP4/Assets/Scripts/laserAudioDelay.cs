using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserAudioDelay : MonoBehaviour
{
    private float elapsedSeconds;
    public float delayTime;
    public AudioSource audioSource;
    public AudioClip laserShortSound;
    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
       audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedSeconds += Time.deltaTime;
        if (elapsedSeconds >= delayTime)
            
            if(!hasPlayed)
            {
                audioSource.PlayOneShot(laserShortSound);
                hasPlayed = true;
            }

    }
}
