using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the purpose of this object is to orchestrate the placement of obstacles and collectables
public class Boss1CourseCreator : MovingManager.CourseCreator
{
    //store if prev obstale filled a lane
    // private bool prevLaneLeft = false;
    // private bool prevLaneMid = false;
    // private bool prevLaneRight = false;
    public Boss1CourseCreator(MovingManager mm) : base(mm){}
    public override List<GameObject> GenerateObstacle()
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

    public override List<GameObject> GenerateCollectable()
    {
        // Calculate the position of the new Collectable
        int lane = Random.Range(0, mm.numberOfLanes)-1;
        float xPos = lane * mm.laneDistance;
        float zPos = mm.nextCollectableGenerationPosition + mm.collectableGenerationDistance;
        string collectableType = getCollectableType();

        // Create the new Collectable
        GameObject newCollectable = mm.collectableFactory.createCollectable(collectableType, new Vector3(xPos, 3.0f, zPos));


        return new List<GameObject>{newCollectable};
    }

    protected override string getObstacleType(){
        return mm.boss.GetComponent<Boss>().getObstacleGenerationType();
    }
    protected override string getCollectableType(){
        return mm.boss.GetComponent<Boss>().getCollectableGenerationType();
    }
}
