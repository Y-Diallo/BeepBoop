using UnityEngine;

public class BlockingObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision was with the player object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger the player collision handler script on the player object
            PlayerCollisionHandler playerCollisionHandler = collision.gameObject.GetComponent<PlayerCollisionHandler>();
            playerCollisionHandler.OnCollisionEnter(collision);
        }
    }
}
