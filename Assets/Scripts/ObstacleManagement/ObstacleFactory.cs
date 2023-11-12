using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private GameObject bigPrefab;
    [SerializeField] private GameObject rampPrefab;

    public GameObject createBigObstacle(Vector3 position){
        var obstacle = Instantiate(bigPrefab, position, Quaternion.identity);
        obstacle.AddComponent<BigObstacle>();
        return obstacle;
    }

    public GameObject createStillBigObstacle(Vector3 position){
        var obstacle = Instantiate(bigPrefab, position, Quaternion.identity);
        obstacle.AddComponent<StillBigObstacle>();
        return obstacle;
    }

    public GameObject createObstacle(string type, UnityEngine.Vector3 position){
        if (type == "big"){
            return createBigObstacle(position);
        } else if (type == "stillBig"){
            return createStillBigObstacle(position);
        }
        return createBigObstacle(position);
    }
}

