using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Laser : MonoBehaviour
{
    public static Transform startPoint;
    public static Transform endPoint;
    private LineRenderer laser;
    private GameObject player; 

    public int spaceBehindPlayer;
    public int spaceInFrontPlayer;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");

        startPoint = player.transform;
        endPoint = player.transform;

        startPoint = GameObject.Find("Behind").transform;
        endPoint = GameObject.Find("InFront").transform;
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z));
        laser.SetPosition(1, new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z));
    }
}
