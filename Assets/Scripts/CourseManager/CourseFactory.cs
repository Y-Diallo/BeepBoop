using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseFactory : MonoBehaviour
{
    public MovingManager.CourseCreator createLevel1BossCourse(MovingManager mm){
        return new Boss1CourseCreator(mm);
    }

    public MovingManager.CourseCreator createCourse(string type, MovingManager mm){
        if (type == "level1"){
            return createLevel1BossCourse(mm);
        } 
        // else if (type == "level2"){
        //     return createLevel2BossCourse(position);
        // }
        return createLevel1BossCourse(mm);
    }
}

