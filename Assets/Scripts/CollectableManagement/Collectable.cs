using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Collectable(){}
    public void moveCollectable(float blockSpeed){
        this.gameObject.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
    }
    public virtual void OnPlayerCollision(GameObject player) {
        Debug.Log("collectable collision with Player");
        this.gameObject.SetActive(false);
    }
}
