using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudioObstacle : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] obstacleSounds;

    public int reactionTime;

    void Start()
    {
        Debug.Log("spatial audio obstacle");
        audioSource.PlayOneShot(SpatialAudioSounds());
        StartCoroutine(ReactionTime());
        Debug.Log("reaction time done");
    }

    // Start is called before the first frame update
    AudioClip SpatialAudioSounds()
    {
        return obstacleSounds[Random.Range(0, obstacleSounds.Length)];
    }

    IEnumerator ReactionTime()
    {
        yield return new WaitForSeconds(reactionTime);
    }
}
