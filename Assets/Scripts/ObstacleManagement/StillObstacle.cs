using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillObstacle : BigObstacle
{
    public StillObstacle() :base(){}
    public override void moveObstacle(float blockSpeed){
        return;
    }
}
