using UnityEngine;

public class FogWallController : MonoBehaviour
{
    public float wallWidth = 10f; // the width of the fog wall
    public float wallHeight = 10f; // the height of the fog wall
    public float wallDistance = 50f; // the distance of the fog wall in front of the player

    private GameObject player; // reference to the player object
    private Vector3 wallPosition; // the position of the fog wall

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // update the position of the fog wall to follow the player
        wallPosition = player.transform.position + player.transform.forward * wallDistance;
        wallPosition.y = 0f; // set the wall position to be at ground level

        // set the fog wall position and scale
        transform.position = wallPosition;
        transform.localScale = new Vector3(wallWidth, wallHeight, 1f);
    }
}
