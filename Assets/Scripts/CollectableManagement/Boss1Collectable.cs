using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Collectable : Collectable
{
    private AudioSource source;
    public Boss1Collectable() :base(){}
    private void Start() {
        source = GameObject.Find("bulletCollectableSFX").GetComponent<AudioSource>();
    }
    public override void OnPlayerCollision(GameObject player) {
        base.OnPlayerCollision(player);
        Debug.Log("boss1 collectable");
        source.Play();
        GameObject.Find("Boss1(Clone)").GetComponent<Boss>().damageBoss();
    }
}
