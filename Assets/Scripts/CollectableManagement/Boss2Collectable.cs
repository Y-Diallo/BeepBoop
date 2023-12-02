using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Collectable : Collectable
{
    private AudioSource source;
    public Boss2Collectable() :base(){}
    private void Start() {
        source = GameObject.Find("bulletCollectableSFX").GetComponent<AudioSource>();
    }
    public override void OnPlayerCollision(GameObject player) {
        base.OnPlayerCollision(player);
        Debug.Log("boss2 collectable");
        source.Play();
        GameObject.Find("Boss2(Clone)").GetComponent<Boss>().damageBoss();
    }
}
