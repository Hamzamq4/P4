using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the primary script for the player's movement in the game. With Touch controls.
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    Vector3 movec = Vector3.zero;
    bool canmove = true;
    int line = 1;
    int targetLine = 1;
    float speed = 20f; // added variable for speed
    float jumpHeight = 20f; // added variable for jump height
    float jumpDuration = 0.5f; // added variable for jump duration
    float gravity = -50f; // added variable for gravity
    private Animator pAnimator;

    // added variables for speed increase
    float maxSpeed = 25f;
    float speedIncreasePerSecond = 0.01f;

    private inputManager inputManager;

    private int trafficLayer;
    private int schoolLayer;
    private GameObject player;

    private void Awake()
    {
        inputManager = GetComponent<inputManager>();
    }

    private void OnEnable()
    {
        inputManager.OnSwipeLeft += OnSwipeLeft;
        inputManager.OnSwipeRight += OnSwipeRight;
        inputManager.OnSwipeUp += OnSwipeUp;
    }

    private void OnDisable()
    {
        inputManager.OnSwipeLeft -= OnSwipeLeft;
        inputManager.OnSwipeRight -= OnSwipeRight;
        inputManager.OnSwipeUp -= OnSwipeUp;
    }
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        pAnimator = GetComponent<Animator>();

        trafficLayer = LayerMask.NameToLayer("TrafficTerrain");
        schoolLayer = LayerMask.NameToLayer("SchoolTerrain");
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (!ScoreManager.isPlayerAlive)
        {
            pAnimator.SetTrigger("Death_b");
            return;
        }
        Vector3 pos = gameObject.transform.position;
        if (!line.Equals(targetLine))
        {
            if (targetLine == 0 && pos.x < -4.1)
            {
                gameObject.transform.position = new Vector3(-2, pos.y, pos.z);
                line = targetLine;
                movec.x = 0;
                canmove = true;
            }
            else if (targetLine == 1 && (pos.x > 0 || pos.x < 0))
            {
                if (line == 0 && pos.x > 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    movec.x = 0;
                    canmove = true;
                }
                else if (line == 2 && pos.x < 0)
                {
                    gameObject.transform.position = new Vector3(0, pos.y, pos.z);
                    line = targetLine;
                    movec.x = 0;
                    canmove = true;
                }
            }
            else if (targetLine == 2 && pos.x > 4.1)
            {
                gameObject.transform.position = new Vector3(2, pos.y, pos.z);
                line = targetLine;
                movec.x = 0;
                canmove = true;
            }
        }

        // added code for continuous movement along the Z axis
        movec.z = speed;

        // added code for gravity
        if (!cc.isGrounded)
        {
            movec.y += gravity * Time.deltaTime;
        }
        cc.Move(movec * Time.deltaTime);

        checkInputs();

        // added code for speed increase
        if (speed < maxSpeed)
        {
            speed += speedIncreasePerSecond * Time.deltaTime;
        }
    }

    IEnumerator Jump()
    {
        Debug.Log("Jump animation started");
        float timeInAir = 0.0f;
        movec.y = jumpHeight;

        while (timeInAir < jumpDuration)
        {
            timeInAir += Time.deltaTime;
            float percentComplete = timeInAir / jumpDuration;
            movec.y = Mathf.Lerp(jumpHeight, 0, percentComplete);
            yield return null;
        }

        pAnimator.ResetTrigger("Jump_b"); // Reset the Jump_b animation trigger
        movec.y = 0.0f;
    }

    void checkInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canmove && line > 0)
        {
            OnSwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && canmove && line < 2)
        {
            OnSwipeRight();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && cc.isGrounded)
        {
            OnSwipeUp();

        }
    }

    private void OnSwipeLeft(){

        if (canmove && line > 0)
        {
            Debug.Log("Left swipe detected"); 
            targetLine--;
            canmove = false;
            movec.x = -15f;
        }   
    }

    private void OnSwipeRight(){

        if (canmove && line < 2)
        {
            Debug.Log("Right swipe detected");
            targetLine++;
            canmove = false;
            movec.x = 15f;
        }
    }

    private void OnSwipeUp(){

        if (cc.isGrounded)
        {
            bool isJumping = true;
            if (isJumping)
            {
                pAnimator.SetTrigger("Jump_b");
            }

            StartCoroutine(Jump());

            Debug.Log("Up swipe detected");
            return;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "trafficcollider")
        {
            player.layer = trafficLayer;
        }
        else if (other.gameObject.tag == "schoolcollider")
        {
            player.layer = schoolLayer;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.layer = LayerMask.NameToLayer("Default");
        }
    }
}
