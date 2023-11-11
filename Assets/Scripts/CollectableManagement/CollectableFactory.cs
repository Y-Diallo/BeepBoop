using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    [SerializeField] private GameObject damagingCollectablePrefab;

    public GameObject createDamagingCollectable(Vector3 position){
        var collectable = Instantiate(damagingCollectablePrefab, position, Quaternion.identity);
        collectable.AddComponent<DamagingCollectable>();
        return collectable;
    }

    public GameObject createCollectable(string type, Vector3 position){
        if (type == "damaging"){
            return createDamagingCollectable(position);
        }
        return createDamagingCollectable(position);
    }
}

