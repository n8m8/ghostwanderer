using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    //Arraylist of the rooms in the level
    public ArrayList rooms;
    //ArrayList of the enemies in the level
    public ArrayList enemies;
    //bool if the conditions are met to load the next level
    public bool levelComplete;
    //True if the player is a ghost, false otherwise
    public bool playerIsGhost;

	// Use this for initialization
	void Start (ArrayList rooms, ArrayList enemies, bool levelComplete, bool playerIsGhost) {
        this.rooms = rooms;
        this.enemies = enemies;
        this.levelComplete = levelComplete;
        this.playerIsGhost = playerIsGhost;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
