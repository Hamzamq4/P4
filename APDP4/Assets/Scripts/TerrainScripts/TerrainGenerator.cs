using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class TerrainGenerator : MonoBehaviour
{   
    // Array of terrain prefabs to randomly choose from
    public GameObject[] terrainPrefabs;

    // Finding player object

    public GameObject player;

    public float deleteDistance = 10f; // Distance behind the player at which objects will be deleted

    private List<GameObject> objectsToDestroy = new List<GameObject>(); // List of objects to destroy
     public GameObject[] objectsToIgnore; // Array of objects to ignore during deletion

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
    private float[] lanePositions = new float[] { -1f, 0f, 1f };

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
        // Calculate the distance between the player and the end of the current terrain section
        float distanceToEdge = currentTerrainLength - player.transform.position.z;

        // If the player is within the distance threshold from the end of the current terrain section, generate a new section
        if (distanceToEdge < distanceThreshold)
        {
            //Destroy(currentTerrain);
            GenerateTerrain();  
        }

        // Next part for deletion of objects behind player
        // Loop through all objects in the scene
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            // Check if the object is behind the player and not in the ignore list
            if (obj.transform.position.z < player.transform.position.z - deleteDistance && !IsObjectIgnored(obj))
            {
                // Add the object to the list of objects to destroy
                objectsToDestroy.Add(obj);
            }
        }

        // Destroy all objects in the list
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        // Clear the list for the next frame
        objectsToDestroy.Clear();
    }

    // Check if an object is in the ignore list
    bool IsObjectIgnored(GameObject obj)
    {
        foreach (GameObject ignoreObj in objectsToIgnore)
        {
            if (obj == ignoreObj)
            {
                return true;
            }
        }

        return false;
    }

    private int? lastTerrainIndex = null;

    void GenerateTerrain()
    {
        // Choose a random terrain prefab from the array, ensuring it's not a Terrain3 prefab
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, terrainPrefabs.Length);
        } while (lastTerrainIndex.HasValue && terrainPrefabs[randomIndex].name == "Terrain3" && lastTerrainIndex.Value == randomIndex);

        // Update lastTerrainIndex to keep track of the previous terrain generated
        lastTerrainIndex = randomIndex;

        // Instantiate the new terrain prefab at the appropriate position
        GameObject newTerrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, 0, currentTerrainLength + 50), Quaternion.identity);

        // Update the current terrain length
        currentTerrainLength += terrainLength + 50;

        // Update the current terrain reference
        currentTerrain = newTerrain;

        // Get the list of obstacle prefabs from the new terrain section
        GameObject[] obstaclePrefabs = newTerrain.GetComponent<TerrainSection>().obstacles;
    }
}








