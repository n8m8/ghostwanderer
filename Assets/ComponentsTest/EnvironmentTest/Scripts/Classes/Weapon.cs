using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    //True if a melee weapon
    public bool melee = false;
    //weapon damage
    public int damage = 0;
    //weapon range
    public int range = 0;
    //Rate of fire of weapon
    public int rate = 0;
    //Last time the weapon was fired
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
