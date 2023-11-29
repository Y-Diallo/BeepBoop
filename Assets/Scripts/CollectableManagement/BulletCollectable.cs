using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollectable : Collectable
{
    private AudioSource source;
    public BulletCollectable() :base(){}
    private void Start() {
        source = GameObject.Find("bulletCollectableSFX").GetComponent<AudioSource>();
    }
    public override void OnPlayerCollision(GameObject player) {
        base.OnPlayerCollision(player);
        Debug.Log("bullet collectable");
        source.Play();
    }
}
