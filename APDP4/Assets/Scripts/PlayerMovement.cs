using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    Vector3 movec = Vector3.zero;
    bool canmove = true;
    int line = 1;
    int targetLine = 1;
    float speed = 25f; // added variable for speed
    float jumpHeight = 20f; // added variable for jump height
    float jumpDuration = 0.5f; // added variable for jump duration
    float gravity = -50f; // added variable for gravity
    private Animator pAnimator;

    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        pAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!ScoreManager.isPlayerAlive)
        {
            // Stop the movement
            movec = Vector3.zero;
            speed = 0f; // Stop the player from moving along the z-axis
            return;
        }

        bool isJumping = (Input.GetKeyDown(KeyCode.UpArrow));
            if (isJumping)
        {
            pAnimator.SetTrigger("Jump_b");
        }

        bool isDucking = (Input.GetKeyDown(KeyCode.DownArrow));
        if (isDucking)
        {
            pAnimator.SetTrigger("Crouch_b");
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
    }

    IEnumerator Jump()
    {
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
            targetLine--;
            canmove = false;
            movec.x = -15f;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && canmove && line < 2)
        {
            targetLine++;
            canmove = false;
            movec.x = 15f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && cc.isGrounded)
        {
            StartCoroutine(Jump());

        }
        
        // added code for ducking on down arrow
        if (Input.GetKeyDown(KeyCode.DownArrow) && cc.isGrounded)
        {
            //cc.height = 1.0f;  // set character height to lower value
            //cc.center = new Vector3(0, -0.5f, 0);  // move character's center down to maintain balance
            
        }
        // added code to reset character height when the down arrow is released
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //cc.height = 2.0f;  // set character height back to default value
            cc.center = new Vector3(0, 0, 0);  // move character's center back to default position
            pAnimator.ResetTrigger("Crouch_b"); // Reset the Jump_b animation trigger
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag=="Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
    void PlayerDeath()
    {
        // Play death animation

        // Set flag to false
        ScoreManager.isPlayerAlive = false;

        // Stop the movement
        movec = Vector3.zero;
    }
}