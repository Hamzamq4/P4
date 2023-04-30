using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalPairs : MonoBehaviour
{
    // Define the arrays of objects
    public GameObject[] objects1;
    public GameObject[] objects2;
    public GameObject[] objects3;
    public GameObject[] objects;
    
    public Transform minimalPairsObject;

    // Define the array of arrays
    public GameObject[][] objectCombinations;

    // Define the array of transforms for the lanes
    public Transform[] laneTransforms;
    public string[] transformNames;
    private bool trueObjectSpawned;

    GameObject trueObject;

    // Start is called before the first frame update
    void Start()
    {
    laneTransforms = new Transform[3]; // create an array with 3 elements
    transformNames = new string[] {"LeftLane", "MiddleLane", "RightLane" }; // set the names of the transforms

    // add Transforms to the array by name
    for (int i = 0; i < laneTransforms.Length; i++)
    {
        GameObject obj = GameObject.Find(transformNames[i]);
        if (obj != null)
        {
            laneTransforms[i] = obj.transform;
        }
    }
        // Populate the array of arrays with the object arrays
        //objectCombinations = new GameObject[][] { objects1, objects2, objects3};
        
        // Choosing a random array
        //int randomIndex = Random.Range(0, objectCombinations.Length);
        //GameObject[] chosenArray = objectCombinations[randomIndex];  

        List<GameObject> availableObjects = new List<GameObject>(objects);
        int randomTrueObjectIndex = Random.Range(0, availableObjects.Count);
        trueObject = availableObjects[randomTrueObjectIndex];
        Debug.Log(trueObject);
        trueObject.tag = "MinimalPairsTrue";

        List<Transform> availableLanes = new List<Transform>(laneTransforms);  
        // Randomly distribute the objects on all three lanes
        for (int i = 0; i < 3; i++)
        {
            if(!trueObjectSpawned)
            {
            int laneForTrueObject = Random.Range(0, availableLanes.Count);
            Vector3 trueObjectPosition = new Vector3(availableLanes[laneForTrueObject].position.x - 1.8f, 6f, minimalPairsObject.position.z + 30f);
            Instantiate(trueObject, trueObjectPosition, Quaternion.identity);
            availableObjects.RemoveAt(randomTrueObjectIndex);
            availableLanes.RemoveAt(laneForTrueObject);
            trueObjectSpawned = true;
            Debug.Log(availableLanes);
            }
            else if (trueObjectSpawned)
            {
            int laneForOtherObjects = Random.Range(0, availableLanes.Count);
            int obstacleToSpawn = Random.Range(0, availableObjects.Count);
            Vector3 objectPosition = new Vector3(availableLanes[laneForOtherObjects].position.x - 1.8f, 6f, minimalPairsObject.position.z + 30f);
            Instantiate(availableObjects[obstacleToSpawn], objectPosition, Quaternion.identity);
            availableObjects.RemoveAt(obstacleToSpawn);
            availableLanes.RemoveAt(laneForOtherObjects);
            Debug.Log("Instantiated object!");
            }
        }
        Invoke("OriginalTag", 8f);
    }

    void OriginalTag()
    {
    trueObject.tag = "Obstacle";
    Debug.Log("Originalt tag nu");
    }

    private void OnDisable() 
    {
    trueObject.tag = "Obstacle";   
    Debug.Log("Slut, Originalt tag nu");
    }
}
