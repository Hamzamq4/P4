using UnityEngine;

public class MinimalPairs : MonoBehaviour
{
    // Define the arrays of objects
    public GameObject[] objects1;
    public GameObject[] objects2;
    public GameObject[] objects3;

    // Define the array of arrays
    public GameObject[][] objectCombinations;

    // Define the array of transforms for the lanes
    public Transform[] laneTransforms;
    public string[] transformNames;

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
        objectCombinations = new GameObject[][] { objects1, objects2, objects3 };

        // Randomly distribute the objects on all three lanes
        for (int i = 0; i < laneTransforms.Length; i++)
        {
            int randomIndex = Random.Range(0, objectCombinations.Length);
            GameObject[] chosenArray = objectCombinations[randomIndex];

            int randomObjectIndex = Random.Range(0, chosenArray.Length);
            Vector3 positionOffset = new Vector3(0f, randomObjectIndex * 1.5f, 0f);
            Vector3 objectPosition = laneTransforms[i].position + positionOffset;
            Instantiate(chosenArray[randomObjectIndex], objectPosition, Quaternion.identity);
            Debug.Log("Instantiated object!");
        }
    }
}