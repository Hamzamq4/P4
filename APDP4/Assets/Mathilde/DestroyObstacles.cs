using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Collision with " + other.gameObject);
            Destroy(other.gameObject);

            ScoreManager.health -= 1;
        }
    }
}
