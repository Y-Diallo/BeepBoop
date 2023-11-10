using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollectable : Collectable
{
    public DamagingCollectable(Vector3 initialPosition, GameObject obstaclePrefab) :base(initialPosition,obstaclePrefab){}
    public override void OnPlayerCollision() {
        base.OnPlayerCollision();
        Debug.Log("damaging Obstacle");
    }
}
