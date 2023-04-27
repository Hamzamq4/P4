using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodAgentBadAgentScript : MonoBehaviour
{
    public GameObject[] obstacles;
    public Transform spatialAudio;

    public Transform[] laneTransforms;
    private string[] transformNames;

    public AudioSource audioSource;
    public AudioClip[] goodGuyAudio;
    public AudioClip[] badGuyAudio;

    public int reactionTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        //int randomLaneIndex = Random.Range(0, laneTransforms.Length);
        int randomLaneIndex = 2;
        Debug.Log("spatial audio obstacle");

        if (randomLaneIndex == 0)
        {
            audioSource.PlayOneShot(goodGuyAudio[0]);
            audioSource.PlayOneShot(badGuyAudio[Random.Range(1 ,3)]);
        }
        else if (randomLaneIndex == 1)
        {
            audioSource.PlayOneShot(goodGuyAudio[1]);
            //choosing badGuyAudio 0 or 2
            int leftOrRight = Random.Range(0, 2);
            if (leftOrRight == 0)
            {
                audioSource.PlayOneShot(badGuyAudio[0]); 
            }
            else
            {
                audioSource.PlayOneShot(badGuyAudio[2]);
            }
        }
        else
        {
            audioSource.PlayOneShot(goodGuyAudio[2]);
            audioSource.PlayOneShot(badGuyAudio[Random.Range(0 ,2)]);
        }
        

        if (Time.time > reactionTime)
        {
            Vector3 objectPosition = new Vector3(laneTransforms[randomLaneIndex].position.x - 1.8f, 4.5f, spatialAudio.position.z - 30f);
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], objectPosition, Quaternion.identity);
        }
    }

    AudioClip SpatialAudioSounds()
    {   
        return goodGuyAudio[Random.Range(0, goodGuyAudio.Length)];
    }
}

