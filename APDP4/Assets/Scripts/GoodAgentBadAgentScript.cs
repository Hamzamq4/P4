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
    public AudioClip[] goodGuyLeft;
    public AudioClip[] goodGuyMiddle;
    public AudioClip[] goodGuyRight;
    public AudioClip[] badGuyLeft;
    public AudioClip[] badGuyMiddle;
    public AudioClip[] badGuyRight; 

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
        
        int freeLaneIndex = Random.Range(0, laneTransforms.Length);
        Debug.Log("spatial audio obstacle");

        // setting positions of lasers in lanes
        Vector3 rightLane = new Vector3(laneTransforms[0].position.x - 1.8f, 4.5f, spatialAudio.position.z - 30f);
        Vector3 middleLane = new Vector3(laneTransforms[1].position.x - 1.8f, 4.5f, spatialAudio.position.z - 30f);
        Vector3 leftLane = new Vector3(laneTransforms[2].position.x - 1.8f, 4.5f, spatialAudio.position.z - 30f);

        if (freeLaneIndex == 0)
        {
            audioSource.PlayOneShot(goodGuyLeft[Random.Range(0, goodGuyLeft.Length)], 0.5f);
            int badGuyRandom = Random.Range(0, 2);

            if (badGuyRandom == 0)
            {
                audioSource.PlayOneShot(badGuyRight[Random.Range(0 , badGuyRight.Length)], 1f);
            }
            else
            {
                audioSource.PlayOneShot(badGuyMiddle[Random.Range(0 , badGuyMiddle.Length)], 1f);
            }

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], rightLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], middleLane, Quaternion.identity);
            }

        }
        else if (freeLaneIndex == 1)
        {
            audioSource.PlayOneShot(goodGuyMiddle[Random.Range(0, goodGuyMiddle.Length)], 0.5f);

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], rightLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], leftLane, Quaternion.identity);
            }
            //choosing badGuyAudio 0 or 2
            int badGuyRandom = Random.Range(0, 2);
            if (badGuyRandom == 0)
            {
                audioSource.PlayOneShot(badGuyLeft[Random.Range(0, badGuyLeft.Length)], 1f); 
            }
            else
            {
                audioSource.PlayOneShot(badGuyRight[Random.Range(0, badGuyRight.Length)], 1f);
            }
        }
        else
        {
            audioSource.PlayOneShot(goodGuyRight[Random.Range(0, goodGuyRight.Length)], 0.5f);

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], leftLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], middleLane, Quaternion.identity);
            }
            
            int badGuyRandom = Random.Range(0, 2);
            if (badGuyRandom == 0)
            {
                audioSource.PlayOneShot(badGuyLeft[Random.Range(0, badGuyLeft.Length)], 1f); 
            }
            else
            {
                audioSource.PlayOneShot(badGuyMiddle[Random.Range(0 , badGuyMiddle.Length)], 1f);
            }

        }
    }
}

