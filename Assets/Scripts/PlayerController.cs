using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneDistance = 2f; // distance between each lane
    [SerializeField] float jumpHeight = 2f; // height of the jump
    [SerializeField] float jumpTime = 1f; // time it takes for the jump to complete

    public bool gameOver = false; // whether the game is over
    private int currentLane = 1; // the lane the player is currently in
    private bool isJumping = false; // whether the player is currently jumping
    private float jumpStartTime; // the time the jump started
    private Vector3 jumpStartPosition; // the starting position of the jump
    private Vector3 jumpTargetPosition; // the target position of the jump
    void Start()
    {
        gameObject.tag = "Player"; // Set the tag of the player object to "Player"
    }
    void Update()
    {
        // Move the player left or right based on input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Debug.Log("horizontalInput is " + horizontalInput);
        if (horizontalInput < 0 && currentLane > 0)
        {
            currentLane--;
            transform.position -= new Vector3(laneDistance, 0, 0);
            Debug.Log("currentLane is " + currentLane + " moving left");
        }
        else if (horizontalInput > 0 && currentLane < 2)
        {
            currentLane++;
            transform.position += new Vector3(laneDistance, 0, 0);
            Debug.Log("currentLane is " + currentLane + " moving right");
        }
        
        // Make the player jump when the space bar is pressed
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            jumpStartTime = Time.time;
            jumpStartPosition = transform.position;
            jumpTargetPosition = transform.position + new Vector3(0, jumpHeight, 0);
        }

        // Handle the player's jump
        if (isJumping)
        {
            float jumpProgress = (Time.time - jumpStartTime) / jumpTime;

            // Move the player towards the target position over time
            transform.position = Vector3.Lerp(jumpStartPosition, jumpTargetPosition, jumpProgress);

            // If the jump is complete, stop jumping and start falling
            if (jumpProgress >= 1f)
            {
                isJumping = false;
                jumpStartTime = Time.time;
                jumpStartPosition = transform.position;
                jumpTargetPosition = transform.position - new Vector3(0, jumpHeight, 0);
            }
        }
        else
        {
            // Handle the player's falling motion when not jumping
            float fallProgress = (Time.time - jumpStartTime) / jumpTime;
            transform.position = Vector3.Lerp(jumpStartPosition, jumpTargetPosition, fallProgress);
        }
    }
}

