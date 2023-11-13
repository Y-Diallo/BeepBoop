using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    [SerializeField] private GameObject damagingCollectablePrefab;
    [SerializeField] private GameObject bulletCollectablePrefab;

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

    public GameObject createCollectable(string type, Vector3 position){
        if (type == "damaging"){
            return createDamagingCollectable(position);
        }
        if (type == "bullet"){
            return createBulletCollectable(position);
        }
        return createDamagingCollectable(position);
    }
}

