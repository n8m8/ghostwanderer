using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjects : MonoBehaviour {

    //NEEDS A COLLIDER
    //NEEDS MOVABLE

    public bool isLocked;

	// Use this for initialization
	void Start (bool isLocked) {
        this.isLocked = isLocked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
