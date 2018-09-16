using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public int level;
    public bool isDiscovered;
    public ArrayList envObjects;
    public ArrayList interactableObjects;

	// Use this for initialization
	void Start (int level, bool isDiscovered, ArrayList envObjects, ArrayList interactableObjects) {
        this.level = level;
        this.isDiscovered = isDiscovered;
        this.envObjects = envObjects;
        this.interactableObjects = interactableObjects;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
