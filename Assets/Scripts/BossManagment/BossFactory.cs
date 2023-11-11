using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFactory : MonoBehaviour
{
    [SerializeField] private GameObject level1BossPrefab;

    public GameObject createLevel1Boss(Vector3 position){
        var boss = Instantiate(level1BossPrefab, position, Quaternion.identity);
        boss.AddComponent<Level1Boss>();
        return boss;
    }

    public GameObject createBoss(string type, Vector3 position){
        if (type == "level1"){
            return createLevel1Boss(position);
        } 
        // else if (type == "level2"){
        //     return createLevel2Boss(position);
        // }
        return createLevel1Boss(position);
    }
}

