using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserAudioDelay : MonoBehaviour
{
    private float elapsedSeconds;
    public AudioSource audioSource;
    public AudioClip laserShortSound;

    // Start is called before the first frame update
    void Start()
    {
       audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedSeconds >= 5f)

        audioSource.PlayOneShot(laserShortSound);

        elapsedSeconds += Time.deltaTime;
    }
}
