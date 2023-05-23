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

            float obstacleHeight = obstacle.GetComponent<Renderer>().bounds.size.y - 1.20f;
            Vector3 lanePos = Vector3.zero;

            // select a lane that is different from the last lane used
            int laneIndex = Random.Range(0, 3);
            while (laneIndex == lastLane)
            {
                laneIndex = Random.Range(0, 3);
            }
            lastLane = laneIndex;

            // instantiate obstacle at the selected lane
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

            float zPos = i * (50f / (numObstacles - 1)) - 25f;
            obstacle.transform.position = new Vector3(lanePos.x, lanePos.y + obstacleHeight, lanePos.z + zPos * laneWidth);
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.transform.localScale = new Vector3(2,2,2);
            obstacle.transform.parent = transform;

            // check if to instantiate the obstacle on one of the other lanes as well
            if (Random.value < 0.3f)
            {
                int otherLaneIndex = (laneIndex + Random.Range(1, 3)) % 3; // choose one of the other two lanes
                Vector3 otherLanePos = Vector3.zero;

                if (otherLaneIndex == 0)
                {
                    otherLanePos = lane1.position;
                }
                else if (otherLaneIndex == 1)
                {
                    otherLanePos = lane2.position;
                }
                else
                {
                    otherLanePos = lane3.position;
                }

                GameObject otherObstacle = Instantiate(obstaclePrefab);
                otherObstacle.transform.position = new Vector3(otherLanePos.x, otherLanePos.y + obstacleHeight, lanePos.z + zPos * laneWidth);
                otherObstacle.transform.rotation = Quaternion.identity;
                otherObstacle.transform.localScale = new Vector3(2, 2, 2);
                otherObstacle.transform.parent = transform;
            }
        }
    }
}