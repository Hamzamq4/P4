using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public GameObject target; // The object we want to follow
    public float followDistance = 15f; // The distance behind the target object we want to follow
    public float acceleration = 50000.0f; // The rate at which we accelerate when moving past the target object

    private Vector3 originalPosition; // The original position of the object holding this script
    private float elapsedSeconds; // The number of seconds that have elapsed since the game started
    private float currentTargetZ;


    // Start is called before the first frame update
    void Start()
    {
        // Store the original position of the object holding this script
        originalPosition = transform.position;
        target = GameObject.Find("Player");
        transform.Rotate(90, 0, 0, Space.Self);
        Destroy(gameObject, 15f); // Destroy the object after 10 second
    }

    // Update is called once per frame
    void Update()
    {
        // Only follow the target if it exists
        if (target != null)
        {
            // Get the target's position
            Vector3 targetPosition = target.transform.position;
            currentTargetZ = target.transform.position.z;

            // If less than 5 seconds have elapsed, follow the target object
            if (elapsedSeconds < 4.5f)
            {
                // Set our position to the target's x and y values, but with our z-coordinate offset by the follow distance
                transform.position = new Vector3(originalPosition.x, originalPosition.y, targetPosition.z - followDistance);
            }
            // Otherwise, accelerate quickly and move past the target object
            else
            {
                float distance = acceleration * Time.deltaTime * Time.deltaTime;
                
                // Move our position ahead of the target object along the z-axis
                transform.position += new Vector3(0f, 0f, distance);
            }

            // Increment the elapsed time
            elapsedSeconds += Time.deltaTime;
        }
    }
}
