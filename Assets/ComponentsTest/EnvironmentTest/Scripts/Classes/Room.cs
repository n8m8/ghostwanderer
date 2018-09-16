using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    //Number of the level the room is in
    public int level;
    //True if the room has been entered
    public bool isDiscovered;
    //Array list of environment objects in the room
    public ArrayList envObjects;
    //Array list of interactable objects in the room 
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
