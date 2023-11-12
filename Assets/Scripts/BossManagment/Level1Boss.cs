using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss : Boss
{
    public Level1Boss() :base(){}
    public override string getObstacleGenerationMode(){
        return "level1";
    }

}
