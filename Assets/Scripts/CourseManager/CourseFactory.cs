using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseFactory : MonoBehaviour
{
    public MovingManager.CourseCreator createLevel1BossCourse(MovingManager mm){
        return new Boss1CourseCreator(mm);
    }
    public MovingManager.CourseCreator createLevel2BossCourse(MovingManager mm){
        return new Boss2CourseCreator(mm);
    }

    public MovingManager.CourseCreator createCourse(string type, MovingManager mm){
        if (type == "boss1"){
            return createLevel1BossCourse(mm);
        } 
        else if (type == "boss2"){
            return createLevel2BossCourse(mm);
        }
        return createLevel1BossCourse(mm);
    }
}

