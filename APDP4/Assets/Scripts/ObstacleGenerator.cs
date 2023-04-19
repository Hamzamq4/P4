using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour 
{

    public Transform[] lanes; // Three lanes
    public float spacing = 5f; // Space between obstacle generations
    public GameObject[] schoolObstacles; // Array of obstacles for school terrain
    public GameObject[] trafficObstacles; // Array of obstacles for traffic terrain
    public float soundObstacleSpacing = 20f; // Space between sound obstacle generations

    private float nextSpawn = 1f; // Time until next obstacle generation
    private int obstacleType = 0; // 0 for school obstacle, 1 for traffic obstacle, 2 for sound obstacle

    private int currentTerrainLayer; // The layer of the currently active terrain
    private Transform player; // The player object

    void Start() 
    {
        // Find the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update () 
    {
        if (Time.time > nextSpawn) 
        {
            Vector2 randomCircle = Random.insideUnitCircle;
            int laneIndex = Mathf.RoundToInt(Mathf.Clamp01(randomCircle.x + 0.5f) * (lanes.Length - 1));

            //int laneIndex = Random.Range(0, lanes.Length); // Pick a random lane to spawn the obstacle in
            Debug.Log(laneIndex);

            // Check the layer of the terrain that the player is about to approach
            RaycastHit hit;
            if (Physics.Raycast(player.position + new Vector3(0, 0, 20f), Vector3.forward, out hit)) 
            {
                currentTerrainLayer = hit.transform.gameObject.layer;
            }

            GameObject obstacleToSpawn;


            if (obstacleType == 2) 
            { // If it's a sound obstacle, pause obstacle generation for longer
                nextSpawn = Time.time + soundObstacleSpacing;
            } 
            else 
            {
                nextSpawn = Time.time + spacing;
            }

            // Determine which obstacles to spawn
            bool spawnOtherLanes = false; // flag to determine if obstacle should be spawned in other lanes

            if (currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain")) 
            { // School terrain
                obstacleToSpawn = schoolObstacles[Random.Range(0, schoolObstacles.Length)];
                spawnOtherLanes = Random.value < 0.1f;
            } 

            else 
            { // Traffic terrain
                obstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                spawnOtherLanes = Random.value < 0.6f;
            }


            GameObject otherObstacleToSpawn = null;
            GameObject thirdObstacleToSpawn = null;

            if (spawnOtherLanes) 
            {
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

                Instantiate(obstacleToSpawn, lanes[laneIndex].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
                Instantiate(thirdObstacleToSpawn, availableLanes[0].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);

                Debug.Log("Double forhindring!");

                spawnOtherLanes = false;
            } 



            else 
            {
                List<Transform> availableLanes = new List<Transform>(lanes);
                availableLanes.RemoveAt(laneIndex);
                int otherLaneIndex = Random.Range(0, availableLanes.Count);
                Transform otherLane = availableLanes[otherLaneIndex];

                if (currentTerrainLayer == LayerMask.NameToLayer("SchoolTerrain")) 
                {
                    thirdObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                    otherObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                } 
                else 
                {
                    thirdObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                    otherObstacleToSpawn = trafficObstacles[Random.Range(0, trafficObstacles.Length)];
                }

                Instantiate(obstacleToSpawn, lanes[laneIndex].position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
                Instantiate(otherObstacleToSpawn, otherLane.position + new Vector3(-1.6f, 4.181f, player.position.z + 50f), Quaternion.identity);
            
            }


        }
    }
}  