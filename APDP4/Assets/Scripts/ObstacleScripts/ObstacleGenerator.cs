using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for the obstacle generation, that handles traffic obstacles, school obstacles and sound obstacles.
/// </summary>

public class ObstacleGenerator : MonoBehaviour 
{
    public Transform[] lanes; // the three lanes
    public float spacing = 5f; // space in units between obstacle generations
    public GameObject[] schoolObstacles; // array of obstacles for school terrain
    public GameObject[] trafficObstacles; // array of obstacles for traffic terrain
    public GameObject[] soundObstacles;// array of sounds obstacles
    public float soundObstacleSpacing = 20f; // space between sound obstacle generations

    private float nextSpawn = 1f; // time in units until next obstacle generation
    private int obstacleType = 0; // 0 for school obstacle, 1 for traffic obstacle, 2 for sound obstacle

    private int currentTerrainLayer; //layer of currently active terrain
    private Transform player; // the player object

    public float spawnStartTime;
    private float elapsedSeconds;

    void Start() 
    {
        // finds the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () 
    {
        elapsedSeconds += Time.deltaTime;
        if (elapsedSeconds >= spawnStartTime)
        {
            if (Time.time > nextSpawn) 
                {
                    Vector2 randomCircle = Random.insideUnitCircle;
                    int laneIndex = Mathf.RoundToInt(Mathf.Clamp01(randomCircle.x + 0.5f) * (lanes.Length - 1));

                    // check the layer of the terrain that the player is about to approach
                    RaycastHit hit;
                    if (Physics.Raycast(player.position + new Vector3(0, -0.1f, 10f), Vector3.forward, out hit)) 
                    {
                        currentTerrainLayer = hit.transform.gameObject.layer;
                        Debug.Log(currentTerrainLayer);
                    }

                    bool isSoundObstacle = Random.value < 0.3f;
                    if (isSoundObstacle) 
                    {
                        obstacleType = 2;
                        nextSpawn = Time.time + soundObstacleSpacing;
                    } 
                    else 
                    {
                        obstacleType = currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain") ? 0 : 1;
                        nextSpawn = Time.time + spacing;
                    }

                    // determines which obstacles to spawn
                    bool spawnOtherLanes = false; // flag to determine if obstacle should be spawned in other lanes

                    GameObject obstacleToSpawn;
                    if (obstacleType == 0) 
                    { // school terrain
                        obstacleToSpawn = schoolObstacles[Random.Range(0, schoolObstacles.Length)];
                        spawnOtherLanes = Random.value < 0.1f;
                    } 
                    else if (obstacleType == 1) 
                    { // traffic terrain
                        obstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                        spawnOtherLanes = Random.value < 0.6f;
                    } 
                    else 
                    { // sound obstacle
                        obstacleToSpawn = soundObstacles[Random.Range(0, soundObstacles.Length)];
                        Debug.Log("Sound Obstacle!");
                    }

                    // spawns the obstacles
                    Instantiate(obstacleToSpawn, lanes[laneIndex].position + new Vector3(-1.6f, 3.9f, player.position.z + 60f), Quaternion.identity);

                    GameObject otherObstacleToSpawn = null;
                    GameObject thirdObstacleToSpawn = null;

                    if (spawnOtherLanes) 
                    {
                        Debug.Log("Double forhindring!");
                        List<Transform> availableLanes = new List<Transform>(lanes);
                        availableLanes.RemoveAt(laneIndex);
                        int otherLaneIndex = Random.Range(0, availableLanes.Count);
                        Transform otherLane = availableLanes[otherLaneIndex];
                        availableLanes.RemoveAt(otherLaneIndex);

                        if (currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain"))
                        {
                            thirdObstacleToSpawn = schoolObstacles[Random.Range(0, schoolObstacles.Length)];
                            otherObstacleToSpawn = schoolObstacles[Random.Range(0, schoolObstacles.Length)];
                        } 
                        else 
                        {
                            thirdObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                            otherObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                        }
                        Instantiate(thirdObstacleToSpawn, availableLanes[0].position + new Vector3(-1.6f, 4.181f, player.position.z + 60f), Quaternion.identity);
                        Instantiate(otherObstacleToSpawn, otherLane.position + new Vector3(-1.6f, 4.181f, player.position.z + 60f), Quaternion.identity);
                        spawnOtherLanes = false;
                    }
                }   
        }
    }
}  