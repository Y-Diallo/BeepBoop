using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    public GameObject movingManager;
    [SerializeField] private GameObject damagingCollectablePrefab;

    public DamagingCollectable createDamagingCollectable(Vector3 position){
        var collectable = movingManager.AddComponent<DamagingCollectable>();
        collectable.setUp(position, damagingCollectablePrefab);
        return collectable;
    }

    public Collectable createCollectable(string type, Vector3 position){
        if (type == "damaging"){
            return createDamagingCollectable(position);
        }
        return createDamagingCollectable(position);
    }
}

