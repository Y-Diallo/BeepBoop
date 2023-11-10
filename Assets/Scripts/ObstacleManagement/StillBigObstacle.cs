using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillBigObstacle : BigObstacle
{
    public StillBigObstacle(Vector3 initialPosition, GameObject obstaclePrefab) :base(initialPosition,obstaclePrefab){}
    public override void moveObstacle(float blockSpeed){
        return;
    }
}
