using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    public GameObject particleSystemPrefab; // Assign the particle system prefab in the Inspector
    public AudioClip destroySound; // Assign the sound in the Inspector

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with " + other.gameObject);
            Destroy(other.gameObject);

            ScoreManager.health -= 1;

            // Instantiate a new particle system at the position of the destroyed gameobject
            Instantiate(particleSystemPrefab, other.gameObject.transform.position, Quaternion.identity);

            // Play the destroy sound
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }
        }
    }
}
