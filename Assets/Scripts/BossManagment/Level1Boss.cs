using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Boss : Boss
{
    public override string getObstacleGenerationType(){
        return "big";
    }
    public override string getCollectableGenerationType(){
        int randomNum = Random.Range(0,2); //0,1
        if (randomNum == 1){
            return "damaging";
        }
        return "boss1";
    }


    public override void bossDeath() {
        base.bossDeath();
        var collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            if(collectable.name == "Boss1Collectable(Clone)"){//consider turning into global var at boss level
                collectable.SetActive(false);
            }
        }
    }
}
