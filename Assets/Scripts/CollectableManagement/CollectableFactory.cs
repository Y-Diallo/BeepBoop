using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    [SerializeField] private GameObject damagingCollectablePrefab;
    [SerializeField] private GameObject bulletCollectablePrefab;
    [SerializeField] private GameObject boss1CollectablePrefab;
    [SerializeField] private GameObject boss2CollectablePrefab;

    public GameObject createDamagingCollectable(Vector3 position){
        var collectable = Instantiate(damagingCollectablePrefab, position, Quaternion.identity);
        collectable.AddComponent<DamagingCollectable>();
        return collectable;
    }
    public GameObject createBulletCollectable(Vector3 position){
        var collectable = Instantiate(bulletCollectablePrefab, position, Quaternion.identity);
        collectable.AddComponent<BulletCollectable>();
        return collectable;
    }
    public GameObject createBoss1Collectable(Vector3 position){
        var collectable = Instantiate(boss1CollectablePrefab, position, Quaternion.identity);
        collectable.AddComponent<Boss1Collectable>();
        return collectable;
    }
    public GameObject createBoss2Collectable(Vector3 position){
        var collectable = Instantiate(boss2CollectablePrefab, position, Quaternion.identity);
        collectable.AddComponent<Boss2Collectable>();
        return collectable;
    }

    public GameObject createCollectable(string type, Vector3 position){
        if (type == "damaging"){
            return createDamagingCollectable(position);
        }
        if (type == "bullet"){
            return createBulletCollectable(position);
        }
        if (type == "boss1"){
            return createBoss1Collectable(position);
        }
        if (type == "boss2"){
            return createBoss2Collectable(position);
        }
        return createDamagingCollectable(position);
    }
}

