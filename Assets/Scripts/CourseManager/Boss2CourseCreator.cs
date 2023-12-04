using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the purpose of this object is to orchestrate the placement of obstacles and collectables
public class Boss2CourseCreator : CourseCreator
{
    //store if prev obstale filled a lane
    // private bool prevLaneLeft = false;
    // private bool prevLaneMid = false;
    // private bool prevLaneRight = false;
    private int rowsSinceLast = 0;
    public Boss2CourseCreator(MovingManager mm) : base(mm){}
    public override List<GameObject> GenerateObstacle()
    {
        List<GameObject> newObstacles = new List<GameObject>();
        if(rowsSinceLast < 2){
            rowsSinceLast += 1;
            return newObstacles;
        } else {
            rowsSinceLast = 0;
        }
        int openLane = Random.Range(0, mm.numberOfLanes)-1;
        string obstacleType = getObstacleType();
        // Calculate the position of the new obstacle
        float zPos = mm.nextObstacleGenerationPosition + mm.obstacleGenerationDistance;
        for(int lane = -1;lane<mm.numberOfLanes-1;lane++){
            float xPos = lane * mm.laneDistance;
            if(lane == openLane){
                newObstacles.Add(mm.obstacleFactory.createObstacle(obstacleType, new Vector3(xPos, 0.0f, zPos)));
            } else {
                newObstacles.Add(mm.obstacleFactory.createObstacle("stillTall", new Vector3(xPos, 0.0f, zPos)));
            }
        }

        return newObstacles;
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

    protected override string getObstacleType(){ //consider storing obstacle types only in course creator
        return mm.boss.GetComponent<Boss>().getObstacleGenerationType();
    }
    protected override string getCollectableType(){
        return mm.boss.GetComponent<Boss>().getCollectableGenerationType();
    }
}
