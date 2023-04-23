using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueObjectScript : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject player;
    public AudioClip audioclip;

    void Start() 
    {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    audioSource = player.GetComponent<AudioSource>();
    }
    void Update()
    {
        if(gameObject.tag == "MinimalPairsTrue")
        {
            audioSource.PlayOneShot(audioclip);
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if this object is the true object
        if (gameObject.tag == "MinimalPairsTrue")
        {
            if (other.gameObject.tag == "Player")
            {
                // Let the player live
                Debug.Log("Player survived");   
            }
            
            // You could add additional code here to notify the player or update the game state
        }

    }
}

