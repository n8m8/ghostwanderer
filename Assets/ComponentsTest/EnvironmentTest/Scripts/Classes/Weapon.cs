using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public bool melee = 0;
    public int damage = 0;
    public int range = 0;
    public int rate = 0;
    public int lastFired = 0;

	// Use this for initialization
	void Start (bool melee, int damage, int range, int rate) {
        this.melee = melee;
        this.damage = damage;
        this.range = range;
        this.rate = rate;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
