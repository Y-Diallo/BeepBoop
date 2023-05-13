using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneChangeSpeed = 5f; // speed of lane change
    [SerializeField] Vector3 leftLanePosition = new Vector3(-3f, 0, 0); // position of left lane
    [SerializeField] Vector3 middleLanePosition = new Vector3(0, 0, 0); // position of middle lane
    [SerializeField] Vector3 rightLanePosition = new Vector3(3f, 0, 0); // position of right lane
    [SerializeField] float jumpHeight = 2f; // height of the jump
    // [SerializeField] float laneChangeCooldown = 0f; // time delay before the player can change lanes again
    private bool canLaneChange = true; // the timer for the lane change cooldown
    private Vector3 targetLanePosition; // the position of the lane the player is moving towards
    public bool gameOver = false; // whether the game is over
    private int currentLane = 1; // the lane the player is currently in
    public bool isJumping = false; // whether the player is currently jumping
    private Rigidbody rigidBody; // the player's rigid body
    private Vector3[] lanePositions = new Vector3[3]; // the positions of the lanes
    void Start()
    {
        gameObject.tag = "Player"; // Set the tag of the player object to "Player"
        rigidBody = GetComponent<Rigidbody>(); // Get the player's rigid body
        targetLanePosition = middleLanePosition;
        lanePositions[0] = leftLanePosition;
        lanePositions[1] = middleLanePosition;
        lanePositions[2] = rightLanePosition;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0 && currentLane > 0 && canLaneChange)
        {
            currentLane--;
            targetLanePosition = lanePositions[currentLane];
            canLaneChange = false;
            StartCoroutine(StartCooldown());
        }
        else if (horizontalInput > 0 && currentLane < 2 && canLaneChange )
        {
            currentLane++;
            targetLanePosition = lanePositions[currentLane];
            canLaneChange = false;
            StartCoroutine(StartCooldown());
        }

        Debug.Log(canLaneChange);
        // Move the player smoothly towards the target lane
        transform.position = Vector3.Lerp(transform.position, targetLanePosition, laneChangeSpeed * Time.deltaTime);
        
        // Make the player jump when the space bar is pressed
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            rigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
    }
    public IEnumerator StartCooldown()
     {
        yield return new WaitForSeconds(0.5f);
        canLaneChange = true;
     }
}

