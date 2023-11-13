using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollectable : Collectable
{
    public BulletCollectable() :base(){}
    public override void OnPlayerCollision(GameObject player) {
        base.OnPlayerCollision(player);
        Debug.Log("bullet collectable");
    }
}
