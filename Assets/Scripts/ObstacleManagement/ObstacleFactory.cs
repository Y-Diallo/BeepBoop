using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    public GameObject movingManager;
    [SerializeField] private GameObject bigPrefab;

    public BigObstacle createBigObstacle(UnityEngine.Vector3 position){
        var obstacle = movingManager.AddComponent<BigObstacle>();
        obstacle.setUp(position, bigPrefab);
        return obstacle;
    }

    public StillBigObstacle createStillBigObstacle(UnityEngine.Vector3 position){
        var obstacle = movingManager.AddComponent<StillBigObstacle>();
        obstacle.setUp(position, bigPrefab);
        return obstacle;
    }

    public Obstacle createObstacle(string type, UnityEngine.Vector3 position){
        if (type == "big"){
            return createBigObstacle(position);
        } else if (type == "stillBig"){
            return createStillBigObstacle(position);
        }
        return createBigObstacle(position);
    }
}

