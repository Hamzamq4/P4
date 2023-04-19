using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

public Transform[] lanes; // Three lanes
public float spacing = 5f; // Space between obstacle generations
public GameObject[] schoolObstacles; // Array of obstacles for school terrain
public GameObject[] trafficObstacles; // Array of obstacles for traffic terrain
public float soundObstacleSpacing = 20f; // Space between sound obstacle generations

private float nextSpawn = 1f; // Time until next obstacle generation
private int obstacleType = 0; // 0 for school obstacle, 1 for traffic obstacle, 2 for sound obstacle

private int currentTerrainLayer; // The layer of the currently active terrain
private Transform player; // The player object

void Start() {
    // Find the player object in the scene
    player = GameObject.FindGameObjectWithTag("Player").transform;
}

void Update () {
    if (Time.time > nextSpawn) {
        int laneIndex = Random.Range(0, lanes.Length); // Pick a random lane to spawn the obstacle in
        Debug.Log(laneIndex);

        // Check the layer of the terrain that the player is about to approach
        RaycastHit hit;
        if (Physics.Raycast(player.position + new Vector3(0, 0, 20f), Vector3.forward, out hit)) {
            currentTerrainLayer = hit.transform.gameObject.layer;
        }

        GameObject obstacleToSpawn;
        GameObject otherObstacleToSpawn;

        if (obstacleType == 2) { // If it's a sound obstacle, pause obstacle generation for longer
            nextSpawn = Time.time + soundObstacleSpacing;
        } else {
            nextSpawn = Time.time + spacing;
        }

        // Determine which obstacles to spawn
        bool spawnOtherLanes = false; // flag to determine if obstacle should be spawned in other lanes
        if (currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain")) { // School terrain
            obstacleToSpawn = schoolObstacles[Random.Range(0, schoolObstacles.Length)];
            spawnOtherLanes = Random.value < 0.1f;
        } else { // Traffic terrain
            obstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
            spawnOtherLanes = Random.value < 0.6f;
        }

        if (spawnOtherLanes) { // 10% chance to spawn in other lanes
            otherObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
            List<Transform> availableLanes = new List<Transform>(lanes);
            availableLanes.RemoveAt(laneIndex); // Remove the selected lane from the list of available lanes

            // Randomly select a lane from the available lanes list
            int otherLaneIndex = Random.Range(0, availableLanes.Count);
            Transform otherLane = availableLanes[otherLaneIndex];

            // Instantiate the obstacle in the selected lane and the other lane
            Instantiate(obstacleToSpawn, lanes[laneIndex].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
            Instantiate(otherObstacleToSpawn, otherLane.position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
            Debug.Log("Double forhindring!");
            // Remove the selected lanes from the list of available lanes
            availableLanes.RemoveAt(otherLaneIndex);
            spawnOtherLanes = false;
        } else { // No obstacle in other lanes
            // Instantiate the obstacle in the selected lane
            Instantiate(obstacleToSpawn, lanes[laneIndex].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
            }
    }
    }
}  