using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    // Array of terrain prefabs to randomly choose from
    public GameObject[] terrainPrefabs;

    // Finding player object

    public GameObject player;

    // Length of each terrain section
    public float terrainLength = 50f;

    // Starting terrain prefab
    public GameObject initialTerrain;

    // Reference to the current terrain section
    private GameObject currentTerrain;

    // Length of the current terrain section
    private float currentTerrainLength;

    // Distance threshold from the end of the current terrain section to generate a new section
    public float distanceThreshold = 10f;

    // Variables for obstacle instantiation
    public float obstacleProbability;
    public float obstaclePlacementDistance;
    public float laneWidth;
    
    private float[] lanePositions = new float[] { -2.8f, 0f, 2.8f };

    void Start()
    {
        // Instantiate the initial terrain prefab at the starting position
        currentTerrain = Instantiate(initialTerrain, Vector3.zero, Quaternion.identity);
        
        // Set the current terrain length to the length of the initial terrain
        currentTerrainLength = terrainLength;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (player.transform.position.z > currentTerrainLength)
        {
        Destroy(currentTerrain);
        GenerateTerrain();
        Debug.Log("Threshold Passed!");
        }
         */

        // Calculate the distance between the player and the end of the current terrain section
        float distanceToEdge = currentTerrainLength - player.transform.position.z;

        // If the player is within the distance threshold from the end of the current terrain section, generate a new section
        if (distanceToEdge < distanceThreshold)
        {
            //Destroy(currentTerrain);
            GenerateTerrain();  
        }
    }
    void GenerateTerrain()
    {
    // Choose a random terrain prefab from the array
    int randomIndex = Random.Range(0, terrainPrefabs.Length);
    GameObject newTerrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, 0, currentTerrainLength + 50), Quaternion.identity);
    float newTerrainZPosition;

    if (currentTerrain != null)
    {
        newTerrainZPosition = currentTerrain.transform.position.z + currentTerrainLength + 50;
    }
    else
    {
        newTerrainZPosition = 0f;
    }

    // Update the current terrain length
    currentTerrainLength += terrainLength + 50;

    // Update the current terrain reference
    currentTerrain = newTerrain;

    // Get the list of obstacle prefabs from the new terrain section
    GameObject[] obstaclePrefabs = newTerrain.GetComponent<TerrainSection>().obstacles;

    // Iterate over the lanes in the new terrain section
    /*for (int i = 0; i < 3; i++)
    {
        // Generate a random number between 0 and 1
        float randomValue = Random.value;

        // If the random number is less than the obstacle probability for this lane, place an obstacle in the lane
        if (randomValue < obstacleProbability)
        {
            // Choose a random obstacle prefab from the list
            int randomObstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstaclePrefab = obstaclePrefabs[randomObstacleIndex];

            // Instantiate the obstacle at a random position in the lane
            float lanePosition = lanePositions[i];
            float obstacleX = lanePosition + Random.Range(-laneWidth / 2f, laneWidth / 2f);
            float obstacleZ = currentTerrain.transform.position.z + obstaclePlacementDistance + Random.Range(-5f, 5f);
            Vector3 obstaclePosition = new Vector3(obstacleX, obstaclePrefab.transform.position.y, obstacleZ);
            Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
        }
    }*/
}
}








