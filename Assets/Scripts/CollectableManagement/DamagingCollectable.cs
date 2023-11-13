using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingCollectable : Collectable
{
    public DamagingCollectable() :base(){}
    public override void OnPlayerCollision(GameObject player) {
        base.OnPlayerCollision(player);
        Debug.Log("damaging collectable");
        player.GetComponent<PlayerController>().doGameOver();
    }
}
