using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Ghost : MonoBehaviour {

    //Timer to show time remaining in ghost form
    public Timer timer;
    //Ghost health, out of 100
    public int health;


	// Use this for initialization
	void Start (Timer timer, int health) {
        this.timer = timer;
        this.health = health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
