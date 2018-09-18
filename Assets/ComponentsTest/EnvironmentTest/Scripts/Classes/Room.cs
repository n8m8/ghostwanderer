using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    //Number of the level the room is in
    [SerializeField] public int level;
    //True if the room has been entered
    [SerializeField] public bool isDiscovered;
    //Array list of environment objects in the room
    [SerializeField] public ArrayList envObjects;
    //Array list of interactable objects in the room 
    [SerializeField] public ArrayList interactableObjects;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
