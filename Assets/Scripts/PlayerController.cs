using System;
using UnityEngine;
using System.Collections;
using Debug = UnityEngine.Debug;

public enum SIDE { LEFT, CENTER, RIGHT };
public enum HITX { LEFT, CENTER, RIGHT, NONE };
public enum HITY { UP, CENTER, DOWN, NONE };
public enum HITZ { FORWARD, CENTER, BACKWARD, NONE };
public class PlayerController : MonoBehaviour
{
    [SerializeField] float laneChangeSpeed = 5f; // speed of lane change
    [SerializeField] float laneDistance = 3f; // distance between lanes
    [SerializeField] float jumpHeight = 15f; // height of the jump
    public SIDE side = SIDE.CENTER; // the side the player is currently on
    public HITX hitX = HITX.NONE;
    public HITY hitY = HITY.NONE;
    public HITZ hitZ = HITZ.NONE;
    public Material playerDead;
    public GameObject gameOverText;
    private CharacterController characterController; // the character controller component of the player
    // private Animator animator; // the animator component of the player
    private float newXPosition; // the x position of the player after a lane change
    private bool canLaneChange = true; // the timer for the lane change cooldown
    public bool gameOver = false; // whether the game is over
    private int currentLane = 1; // the lane the player is currently in
    // private bool InJump = false; // whether the player is currently jumping
    // private bool InRoll = false; // whether the player is currently rolling
    public float x; // the x position of the player
    public float y; // the y position of the player
    public float z; // the z position of the player
    void Start()
    {
        gameObject.tag = "Player"; // Set the tag of the player object to "Player"
        characterController = GetComponent<CharacterController>(); // Get the character controller component of the player

    }

    void Update()
    {
        if(gameOver){
            return;
        }
        characterController.Move(Vector3.forward * Time.deltaTime * 10f);
        z = transform.position.z;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0 && canLaneChange)
        {
            currentLane--;
            if(side == SIDE.CENTER)
            {
                side = SIDE.LEFT;
                newXPosition = -laneDistance;
            }
            else if(side == SIDE.RIGHT)
            {
                side = SIDE.CENTER;
                newXPosition = 0;
            }
            
            StartCoroutine(StartCooldown());
        }
        else if (horizontalInput > 0 && canLaneChange )
        {
            currentLane++;
            if (side == SIDE.CENTER)
            {
                side = SIDE.RIGHT;
                newXPosition = laneDistance;
            }
            else if (side == SIDE.LEFT)
            {
                side = SIDE.CENTER;
                newXPosition = 0;
            }
            StartCoroutine(StartCooldown());
        }
        Vector3 moveVector = new Vector3(x - transform.position.x, y*Time.deltaTime, 0);
        x = Mathf.Lerp(x, newXPosition, laneChangeSpeed * Time.deltaTime);
        characterController.Move(moveVector);
        Jump();

    }

    public void doGameOver(){
        gameOver = true;
        GetComponent<MeshRenderer> ().material = playerDead;
        //show gameover
        gameOverText.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Floor"){
            return;
        } else if (other.transform.tag == "Collectable"){
            return;
        }
        Debug.Log("hit obstacle");
        OnCharacterColliderHit(other.collider);
    }

    public void OnCharacterColliderHit(Collider col){
        hitX = GetHitX(col);
        //determine correct punishment

        doGameOver();
    }

    public HITX GetHitX(Collider col){
        Bounds characterBounds = characterController.bounds;
        Bounds colliderBounds = characterController.bounds;
        float minX = Mathf.Max(colliderBounds.min.x,characterBounds.min.x);
        float maxX = Mathf.Max(colliderBounds.max.x,characterBounds.max.x);
        float average = (maxX+minX)/2f - colliderBounds.min.x;
        HITX hit;
        if(average>colliderBounds.size.x - 0.33f){
            hit = HITX.RIGHT;
        } else if (average < 0.33f){
            hit = HITX.LEFT;
        } else {
            hit = HITX.CENTER;
        }
        return hit;
    }
    public void Jump()
    {
        if (characterController.isGrounded)
        {
           if (Input.GetButtonDown("Jump"))
            {
                y = jumpHeight;
                // InJump = true;
            } 
        }else
        {
            y -= jumpHeight * 2 * Time.deltaTime;
            // InJump = false;
        }
    }
    public System.Collections.IEnumerator StartCooldown()
     {
        canLaneChange = false;
        yield return new WaitForSeconds(0.15f);
        canLaneChange = true;
     }
}

