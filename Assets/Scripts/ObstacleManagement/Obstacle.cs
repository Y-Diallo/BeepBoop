using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject obstacle;
    public Obstacle(Vector3 initialPosition, GameObject obstaclePrefab){
        obstacle = Instantiate(obstaclePrefab, initialPosition, Quaternion.identity);
    }

    public void setUp(Vector3 initialPosition, GameObject obstaclePrefab){
        obstacle = Instantiate(obstaclePrefab, initialPosition, Quaternion.identity);
    }
    public void moveObstacle(float blockSpeed){
        obstacle.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
    }

}
