using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    //Arraylist of the rooms in the level
    [SerializeField] public ArrayList rooms;
    //ArrayList of the enemies in the level
    [SerializeField] public ArrayList enemies;
    //bool if the conditions are met to load the next level
    [SerializeField] public bool levelComplete;
    //True if the player is a ghost, false otherwise
    [SerializeField] public bool playerIsGhost;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
