using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private GameObject collectable;
    public Collectable(Vector3 initialPosition, GameObject collectablePrefab){
        collectable = Instantiate(collectablePrefab, initialPosition, Quaternion.identity);
    }

    public void setUp(Vector3 initialPosition, GameObject collectablePrefab){
        collectable = Instantiate(collectablePrefab, initialPosition, Quaternion.identity);
    }
    public void moveCollectable(float blockSpeed){
        collectable.transform.Translate(Vector3.back * blockSpeed * Time.deltaTime);
    }
    public virtual void OnPlayerCollision() {
        Debug.Log("collision with Player");
        collectable.SetActive(false);
    }
}
