
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public LayerMask blockingLayer; // the layer containing the blocking game objects
    public LayerMask groundLayer; // the layer containing the ground game objects

    public void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Collision detected");
        // // Check if the collision was with a blocking object
        // Debug.Log(collision.gameObject.layer);
        // if ((blockingLayer.value.Equals(collision.gameObject.layer)))
        // {
        //     // Reset the player's position to the center lane and ground level
        //     PlayerController playerController = GetComponent<PlayerController>();
        //     // playerController.currentLane = 1;
        //     // transform.position = new Vector3(0, 0, 0);

        //     // Reset the player's jump state
        //     // playerController.isJumping = false;
        //     // playerController.jumpStartTime = Time.time;
        //     // playerController.jumpStartPosition = transform.position;
        //     // playerController.jumpTargetPosition = transform.position - new Vector3(0, playerController.jumpHeight, 0);
        //     playerController.gameOver = true;
        // }
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("player is colliding");
        if (other.gameObject.CompareTag("Collectable")) {
            other.gameObject.GetComponent<Collectable>().OnPlayerCollision(this.gameObject);
        } else if(other.gameObject.CompareTag("Obstacle")){
            other.gameObject.GetComponent<Obstacle>().OnPlayerCollision(this.gameObject);
        }
    }
}
