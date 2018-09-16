using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Health out of 100
    public int health;

    public Weapon weapon;

	// Use this for initialization
	void Start (int health, Weapon weapon) {
        this.health = health;
        this.weapon = weapon;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
