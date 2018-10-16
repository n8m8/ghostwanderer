using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	[SerializeField] private LevelController levelController;

	// Use this for initialization
	void Start () {
		levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Checkpoint system
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "TestPlayer"){
			// Debug.Log("Jake vent");
			levelController.currentCheckpoint = gameObject;
		}
	}
}
