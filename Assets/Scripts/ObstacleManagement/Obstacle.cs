using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Obstacle(){}
    public virtual void moveObstacle(float blockSpeed){
        this.gameObject.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
    }
    public virtual void OnPlayerCollision(GameObject player) {
        Debug.Log("obstacle collision with Player");
        // this.gameObject.SetActive(false);
    }
}
