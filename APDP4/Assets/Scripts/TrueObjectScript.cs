using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueObjectScript : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject player;
    public AudioClip audioclip;
    public AudioClip[] feedbackClip;

    void Start() 
    {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    audioSource = player.GetComponent<AudioSource>();

    if(gameObject.tag == "MinimalPairsTrue")
        {
            audioSource.PlayOneShot(audioclip);
        }
    }
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if this object is the true object
        if (gameObject.tag == "MinimalPairsTrue")
        {
            if (other.gameObject.tag == "Player")
            {
                audioSource.PlayOneShot(feedbackClip[Random.Range(0, feedbackClip.Length)]);
                Destroy(gameObject);
                Debug.Log("Player survived");   
            }
            
            // You could add additional code here to notify the player or update the game state
        }

    }
}

