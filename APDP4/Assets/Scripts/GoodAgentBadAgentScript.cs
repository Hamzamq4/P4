using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodAgentBadAgentScript : MonoBehaviour
{
    public GameObject[] obstacles;
    public Transform spatialAudio;

    public Transform[] laneTransforms;
    private string[] transformNames;

    public AudioSource radio;
    public AudioSource radioLeft;
    public AudioSource radioRight;

    public List<AudioSource> leftOrRight;

    public AudioClip[] goodGuyLeft;
    public AudioClip[] goodGuyMiddle;
    public AudioClip[] goodGuyRight;
    public AudioClip[] badGuyLeft;
    public AudioClip[] badGuyMiddle;
    public AudioClip[] badGuyRight; 

    public int reactionTime;

    void Start()
    {
        GameObject radioObject = GameObject.FindGameObjectWithTag("Radio");
        radio = radioObject.GetComponent<AudioSource>();
        
        GameObject radioLeftObject = GameObject.FindGameObjectWithTag("RadioLeft");
        radioLeft = radioLeftObject.GetComponent<AudioSource>();

        GameObject radioRightObject = GameObject.FindGameObjectWithTag("RadioRight");
        radioRight = radioRightObject.GetComponent<AudioSource>();

        leftOrRight.Add(radioLeft);
        leftOrRight.Add(radioRight);
        int randomRadioIndex = Random.Range(0, leftOrRight.Count);


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
            Debug.Log(leftOrRight[randomRadioIndex]);
            leftOrRight[randomRadioIndex].PlayOneShot(goodGuyLeft[Random.Range(0, goodGuyLeft.Length)], 10f);
            leftOrRight.RemoveAt(randomRadioIndex);
            int badGuyRandom = Random.Range(0, 2);

            if (badGuyRandom == 0)
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyRight[Random.Range(0 , badGuyRight.Length)], 5f);
            }
            else
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyMiddle[Random.Range(0 , badGuyMiddle.Length)], 5f);
            }

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], rightLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], middleLane, Quaternion.identity);
            }

        }
        else if (freeLaneIndex == 1)
        {
            leftOrRight[randomRadioIndex].PlayOneShot(goodGuyMiddle[Random.Range(0, goodGuyMiddle.Length)], 10f);
            leftOrRight.RemoveAt(randomRadioIndex);

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], rightLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], leftLane, Quaternion.identity);
            }
            //choosing badGuyAudio 0 or 2
            int badGuyRandom = Random.Range(0, 2);
            if (badGuyRandom == 0)
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyLeft[Random.Range(0 , badGuyLeft.Length)], 5f); 
            }
            else
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyRight[Random.Range(0 , badGuyRight.Length)], 5f);
            }
        }
        else
        {
            leftOrRight[randomRadioIndex].PlayOneShot(goodGuyRight[Random.Range(0, goodGuyRight.Length)], 10f);
            leftOrRight.RemoveAt(randomRadioIndex);

            if (Time.time > reactionTime)
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], leftLane, Quaternion.identity);
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], middleLane, Quaternion.identity);
            }
            
            int badGuyRandom = Random.Range(0, 2);
            if (badGuyRandom == 0)
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyLeft[Random.Range(0 , badGuyLeft.Length)], 5f);
            }
            else
            {
                leftOrRight[Random.Range(0,leftOrRight.Count)].PlayOneShot(badGuyMiddle[Random.Range(0 , badGuyMiddle.Length)], 5f);
            }

        }
    }
}

