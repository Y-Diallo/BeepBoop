using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Boss : Boss
{
    public override string getObstacleGenerationType(){
        return "stillBig";
    }
    public override string getCollectableGenerationType(){
        int randomNum = Random.Range(0,3); //0,1,2
        if (randomNum >= 1){ //2/3 66%
            return "damaging";
        }
        return "boss2";
    }


    public override void bossDeath() {
        base.bossDeath();
        var collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            if(collectable.name == "Boss2Collectable(Clone)"){//consider turning into global var at boss level
                collectable.SetActive(false);
            }
        }
    }
}
