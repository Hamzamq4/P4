using UnityEngine;

public class FogWall : MonoBehaviour
{
    public GameObject player; // The player character
    public GameObject fogWall; // The fog wall prefab
    public float wallDistance = 50f; // The distance in front of the player to place the wall

    private void Start()
    {
        // Spawn the fog wall and position it in front of the player
        fogWall = Instantiate(fogWall);
        fogWall.transform.position = player.transform.position + player.transform.forward * wallDistance;
    }

    private void Update()
    {
        // Move the player character forward
        player.transform.Translate(Vector3.forward * Time.deltaTime);

        // Move the fog wall in front of the player
        fogWall.transform.position = player.transform.position + player.transform.forward * wallDistance;
    }
}
