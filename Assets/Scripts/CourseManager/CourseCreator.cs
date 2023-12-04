using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseCreator
{
    //store if prev obstale filled a lane
    // private bool prevLaneLeft = false;
    // private bool prevLaneMid = false;
    // private bool prevLaneRight = false;
    protected MovingManager mm;
    public CourseCreator(MovingManager mm){
        this.mm = mm;
    }
    public virtual List<GameObject> GenerateObstacle()
    {
        // Calculate the position of the new obstacle
        int lane = Random.Range(0, mm.numberOfLanes)-1;
        float xPos = lane * mm.laneDistance;
        float zPos = mm.nextObstacleGenerationPosition + mm.obstacleGenerationDistance;
        string obstacleType = getObstacleType();

        // Create the new obstacle
        GameObject newObstacle = mm.obstacleFactory.createObstacle(obstacleType, new Vector3(xPos, 0.0f, zPos));

        return new List<GameObject>{newObstacle};
    }

    public virtual List<GameObject> GenerateCollectable()
    {
        // Calculate the position of the new obstacle
        int lane = Random.Range(0, mm.numberOfLanes)-1;
        float xPos = lane * mm.laneDistance;
        float zPos = mm.nextCollectableGenerationPosition + mm.collectableGenerationDistance;
        string collectableType = getCollectableType();

        // Create the new obstacle
        GameObject newCollectable = mm.collectableFactory.createCollectable(collectableType, new Vector3(xPos, 3.0f, zPos));

        return new List<GameObject>{newCollectable};
    }

    protected virtual string getObstacleType(){
        return "stillBig";
    }
    protected virtual string getCollectableType(){
        return "bullet";
    }
}