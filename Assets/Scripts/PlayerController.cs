using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneDistance = 3f; // distance between each lane
    [SerializeField] float jumpHeight = 2f; // height of the jump

    public bool gameOver = false; // whether the game is over
    private int currentLane = 1; // the lane the player is currently in
    public bool isJumping = false; // whether the player is currently jumping
    private Rigidbody rigidBody; // the player's rigid body
    void Start()
    {
        gameObject.tag = "Player"; // Set the tag of the player object to "Player"
        rigidBody = GetComponent<Rigidbody>(); // Get the player's rigid body
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
            Debug.Log("Player position is " + transform.position);
            Debug.Log("currentLane is " + currentLane + " moving left");
        }
        else if (horizontalInput > 0 && currentLane < 2)
        {
            currentLane++;
            transform.position += new Vector3(laneDistance, 0, 0);
            Debug.Log("Player position is " + transform.position);
            Debug.Log("currentLane is " + currentLane + " moving right");
        }
        
        // Make the player jump when the space bar is pressed
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }
}

