using UnityEngine;

public class BlockingObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("BOX Collision detected");
        // Check if the collision was with the player object
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER BOX Collision detected");
            
            // Trigger the player collision handler script on the player object
            PlayerCollisionHandler playerCollisionHandler = collision.gameObject.GetComponent<PlayerCollisionHandler>();
            playerCollisionHandler.OnCollisionEnter(collision);
        }
    }
}
