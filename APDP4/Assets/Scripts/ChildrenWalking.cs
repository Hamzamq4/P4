using UnityEngine;

public class ChildrenWalking : MonoBehaviour
{
    public float speed = 5f;  // speed of movement in -z direction
    public Animator animator;  // reference to Animator component

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, -speed);
        Destroy(gameObject, 10f);  // destroy after 10 seconds
        //animator.SetTrigger("");  // trigger animation
    }
}
