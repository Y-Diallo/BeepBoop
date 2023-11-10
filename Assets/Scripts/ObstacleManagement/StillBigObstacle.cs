using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillBigObstacle : BigObstacle
{
    public StillBigObstacle() :base(){}
    public override void moveObstacle(float blockSpeed){
        return;
    }
}
