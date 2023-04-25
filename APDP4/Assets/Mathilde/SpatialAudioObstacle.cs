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

    public int reactionTime;

    void Start()
    {
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
        audioSource.PlayOneShot(SpatialAudioSounds());
        StartCoroutine(ReactionTime());
        Debug.Log("reaction time done");
        Obstacle();
    }

    AudioClip SpatialAudioSounds()
    {
        return obstacleSounds[Random.Range(0, obstacleSounds.Length)];
    }

    void Obstacle()
    {
        for (int i = 0; i < laneTransforms.Length; i++)
        {
            Vector3 objectPosition = new Vector3(laneTransforms[i].position.x - 1.8f, 4.5f, spatialAudio.position.z + 30f);
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], objectPosition, Quaternion.identity);
        }
    }

    IEnumerator ReactionTime()
    {
        yield return new WaitForSeconds(reactionTime);
    }
}
