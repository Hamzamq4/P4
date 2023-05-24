using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script works as the main terrain generation, which generates the different terrains that
/// procedurally appear in front of the player. 
/// </summary>

public class TerrainGenerator : MonoBehaviour
{   
    public GameObject[] terrainPrefabs;
    public GameObject player;

    public float deleteDistance = 10f; // distance behind the player where the objects will be deleted

    private List<GameObject> objectsToDestroy = new List<GameObject>(); // list of objects to be destroyed
    public GameObject[] objectsToIgnore; // array of objects that shouldnt be deleted

    public float terrainLength = 50f;

    // starting terrain prefab
    public GameObject initialTerrain;

    // reference to the current terrain section
    private GameObject currentTerrain;

    // current length of terrain section
    private float currentTerrainLength;

    // distance from the end of the current terrain section for generating a new section
    public float distanceThreshold = 10f;

    // variables for obstacle instantiation
    public float obstacleProbability;
    public float obstaclePlacementDistance;
    public float laneWidth;
    private float[] lanePositions = new float[] { -1f, 0f, 1f };

    void Start()
    {
        // instantiate the initial terrain prefab at the starting position
        currentTerrain = Instantiate(initialTerrain, Vector3.zero, Quaternion.identity);
        
        // sets current terrain length to length of the initial terrain
        currentTerrainLength = terrainLength;
    }

    void Update()
    {
        // calculates the distance between the player and the end of the current terrain section
        float distanceToEdge = currentTerrainLength - player.transform.position.z;

        // if the player is within the distance threshold from the end of the current terrain section it generates a new section
        if (distanceToEdge < distanceThreshold)
        {
            GenerateTerrain();  
        }

        // for deletion of objects behind player - loop through all objects in the scene
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            // checks if the object is behind the player and not in the ignore list
            if (obj.transform.position.z < player.transform.position.z - deleteDistance && !IsObjectIgnored(obj))
            {
                // adds the object to the list of objects to destroy
                objectsToDestroy.Add(obj);
            }
        }

        // destroys all objects in the list
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
        objectsToDestroy.Clear();
    }

    // check if an object is in the ignore list
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
        // choose a random terrain prefab from the array, ensuring it's not a Terrain3 prefab
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, terrainPrefabs.Length);
        } while (lastTerrainIndex.HasValue && terrainPrefabs[randomIndex].name == "Terrain3" && lastTerrainIndex.Value == randomIndex);

        // update lastTerrainIndex to keep track of the previous terrain generated
        lastTerrainIndex = randomIndex;

        // instantiate the new terrain prefab at the appropriate position
        GameObject newTerrain = Instantiate(terrainPrefabs[randomIndex], new Vector3(0, 0, currentTerrainLength + 50), Quaternion.identity);

        // updates the current terrain length
        currentTerrainLength += terrainLength + 50;

        // updates the current terrain reference
        currentTerrain = newTerrain;

        // gets list of obstacle prefabs from the new terrain section
        GameObject[] obstaclePrefabs = newTerrain.GetComponent<TerrainSection>().obstacles;
    }
}








