using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimalPairs : MonoBehaviour
{
    // Define the arrays of objects
    public GameObject[] objects1;
    public GameObject[] objects2;
    public GameObject[] objects3;
    
    public Transform minimalPairsObject;

    // Define the array of arrays
    public GameObject[][] objectCombinations;

    // Define the array of transforms for the lanes
    public Transform[] laneTransforms;
    public string[] transformNames;

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
        objectCombinations = new GameObject[][] { objects1, objects2, objects3};
        
        // Choosing a random array
        int randomIndex = Random.Range(0, objectCombinations.Length);
        GameObject[] chosenArray = objectCombinations[randomIndex];  

        trueObject = chosenArray[Random.Range(0, chosenArray.Length)];
        Debug.Log(trueObject);
        trueObject.tag = "MinimalPairsTrue";

        List<GameObject> availableObjects = new List<GameObject>(chosenArray);  
        // Randomly distribute the objects on all three lanes
        for (int i = 0; i < laneTransforms.Length; i++)
        {
            int obstacleToSpawn = Random.Range(0, availableObjects.Count);
            Vector3 objectPosition = new Vector3(laneTransforms[i].position.x - 1.8f, 6f, minimalPairsObject.position.z + 30f);
            //Quaternion objectRotation = availableObjects[obstacleToSpawn].transform.rotation; // Get the rotation of the prefab
            Instantiate(availableObjects[obstacleToSpawn], objectPosition, Quaternion.identity);
            availableObjects.RemoveAt(obstacleToSpawn);
            Debug.Log("Instantiated object!");
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
