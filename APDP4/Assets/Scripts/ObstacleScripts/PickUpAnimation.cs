using UnityEngine;

public class PickUpAnimation : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float bopHeight = 0.2f;
    public float bopSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = startPos.y + Mathf.Sin(Time.time * bopSpeed) * bopHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}