using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {

    public Transform[] lanes;
    public float spacing = 5f; // Space between obstacle generations
    public GameObject[] schoolObstacles; // Array of obstacles for school terrain
    public GameObject[] trafficObstacles; // Array of obstacles for traffic terrain
    public GameObject[] soundObstacles; // Array of sound obstacles
    public float soundObstacleSpacing = 20f; // Space between sound obstacle generations

    private float nextSpawn = 0f; // Time until next obstacle generation
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
            int otherLaneIndex = (laneIndex + 1 + Random.Range(0, lanes.Length - 1)) % lanes.Length; // Pick another random lane index that is not the same as the first

            // Check the layer of the terrain that the player is about to approach
            RaycastHit hit;
            if (Physics.Raycast(player.position + new Vector3(0, 0, 20f), Vector3.forward, out hit)) {
                currentTerrainLayer = hit.transform.gameObject.layer;
            }

            GameObject obstacleToSpawn;

            if (obstacleType == 2) { // If it's a sound obstacle, pause obstacle generation for longer
                nextSpawn = Time.time + soundObstacleSpacing;
            } else {
                nextSpawn = Time.time + spacing;
            }

            if (Random.value < 0.1f) { // 10% chance of spawning a sound obstacle instead
                obstacleToSpawn = soundObstacles[Random.Range(0, soundObstacles.Length)];
                obstacleType = 2;
            } else {
                obstacleType = currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain") ? 0 : 1;
                obstacleToSpawn = currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain") ? 
                    schoolObstacles[Random.Range(0, schoolObstacles.Length)] : trafficObstacles[Random.Range(0, trafficObstacles.Length)];
            }

            // Instantiate the obstacle in the selected lane and possibly another random lane
            for (int i = 0; i < lanes.Length; i++) {
                if (i == laneIndex || i == otherLaneIndex) {
                    Vector3 spawnPosition = lanes[i].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f);
                    Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}