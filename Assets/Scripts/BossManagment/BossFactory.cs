using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFactory : MonoBehaviour
{
    [SerializeField] private GameObject boss1Prefab;
    [SerializeField] private GameObject boss2Prefab;

    public GameObject createLevel1Boss(Vector3 position){
        var boss = Instantiate(boss1Prefab, position, Quaternion.identity);
        boss.AddComponent<Level1Boss>();
        return boss;
    }
    public GameObject createLevel2Boss(Vector3 position){
        var boss = Instantiate(boss2Prefab, position, Quaternion.identity);
        boss.AddComponent<Level2Boss>();
        return boss;
    }

    public GameObject createBoss(string type, Vector3 position){
        if (type == "boss1"){
            return createLevel1Boss(position);
        } 
        else if (type == "boss2"){
            return createLevel2Boss(position);
        }
        return createLevel1Boss(position);
    }
}

