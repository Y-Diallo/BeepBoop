using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Boss(){}
    private bool movingRight = true; // Initial direction.
    public float speed = 2.0f; // Adjust the speed as needed.

    private float laneDistance = 3f; // TODO distance between lanes is duplicated from
    // TODO adjust to move boss backward automatically and update a playerspeed var
    public void moveBoss(float playerSpeed){
        this.gameObject.transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);
    }
    public virtual string getObstacleGenerationMode(){
        return "default";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make it auto magically move left and right
        // Move the object smoothly between the left and right limits.
        if (transform.position.x >= laneDistance){
            movingRight = false;
        }
        else if (transform.position.x <= -laneDistance){
            movingRight = true;
        }

        // Calculate the movement direction based on the current direction.
        float direction = movingRight ? 1 : -1;

        // Calculate the new position.
        float newPositionX = transform.position.x + direction * speed * Time.deltaTime;

        // Clamp the position to stay within the defined limits.
        newPositionX = Mathf.Clamp(newPositionX, -laneDistance, laneDistance);

        // Update the object's position.
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

    }  

}
