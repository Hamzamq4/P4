using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudioObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    public Transform spatialAudio;

    private Transform[] laneTransforms;
    private string[] transformNames;

    public AudioSource audioSource;
    public AudioClip[] obstacleSounds;
    public GameObject radio;

    public int reactionTime;

    void Start()
    {
        GameObject radio = GameObject.FindGameObjectWithTag("Radio");
        audioSource = radio.GetComponent<AudioSource>();

        laneTransforms = new Transform[3]; // create an array with 3 elements
        transformNames = new string[] { "LeftLane", "MiddleLane", "RightLane" }; // set the names of the transforms

        for (int i = 0; i < laneTransforms.Length; i++)
        {
            GameObject obj = GameObject.Find(transformNames[i]);
            if (obj != null)
            {
                laneTransforms[i] = obj.transform;
            }
        }

        Debug.Log("spatial audio obstacle");
        audioSource.PlayOneShot(SpatialAudioSounds(), 1f);
        StartCoroutine(ReactionTime());
        Debug.Log("reaction time done");
        
    }

    AudioClip SpatialAudioSounds()
    {
        return obstacleSounds[Random.Range(0, obstacleSounds.Length)];
    }

    IEnumerator ReactionTime()
    {
        yield return new WaitForSeconds(reactionTime);

        int randomLaneIndex = Random.Range(0, laneTransforms.Length);
        Vector3 objectPosition = new Vector3(laneTransforms[randomLaneIndex].position.x - 1.8f, 4.5f, spatialAudio.position.z - 30f);
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], objectPosition, Quaternion.identity);
    }
}
