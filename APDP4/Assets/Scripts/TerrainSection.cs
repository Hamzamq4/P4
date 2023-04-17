using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSection : MonoBehaviour
{
    public GameObject[] obstacles;
    public Transform lane1;
    public Transform lane2;
    public Transform lane3;

    public float laneWidth; // adjust this as needed
    public int minObstacles; // adjust these as needed
    public int maxObstacles = 3;

    void Start()
    {
        // determine how many obstacles to create
        int numObstacles = Random.Range(minObstacles, maxObstacles + 1);

        // instantiate obstacles along all three lanes
        int lastLane = -1;
        for (int i = 0; i < numObstacles; i++)
        {
            float laneSelector = Random.value;
            GameObject obstaclePrefab = obstacles[Random.Range(0, obstacles.Length)];
            GameObject obstacle = Instantiate(obstaclePrefab);

            float obstacleHeight = obstacle.GetComponent<Renderer>().bounds.size.y / 2f;
            Vector3 lanePos = Vector3.zero;

            // select a lane that is different from the last lane used
            int laneIndex = Random.Range(0, 3);
            while (laneIndex == lastLane)
            {
                laneIndex = Random.Range(0, 3);
            }
            lastLane = laneIndex;

            if (laneIndex == 0)
            {
                lanePos = lane1.position;
            }
            else if (laneIndex == 1)
            {
                lanePos = lane2.position;
            }
            else
            {
                lanePos = lane3.position;
            }

            float zPos = i * (40f / (numObstacles - 1)) - 20f;
            obstacle.transform.position = new Vector3(lanePos.x, lanePos.y + obstacleHeight, lanePos.z + zPos * laneWidth);
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.transform.localScale = Vector3.one;
            obstacle.transform.parent = transform;
        }
    }
}