using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float distance = 100f; // Distance to move the object on the z-axis
    public float speed = 120f; // Speed at which the object moves

    //public GameObject particleSystemPrefab; // Assign the particle system prefab in the Inspector


    private void Start()
    {
        transform.Translate(0, 0, -distance); // Move the object on the z-axis
        transform.Rotate(Vector3.up, 0f); // Rotate the object 180 degrees around the y-axis
        Destroy(gameObject, 10f); // Destroy the object after 10 second
    }

    private void Update()
    {
        transform.Translate(0, 0, -speed * Time.deltaTime); // Move the object on the z-axis
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("schoolcollider"))
        {
            // Instantiate a new particle system at the position of the destroyed gameobject
            //Instantiate(particleSystemPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
