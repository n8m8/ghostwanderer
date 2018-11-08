using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUps : MonoBehaviour {

	public GameObject textUI;
	void Start(){
		textUI.GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			 textUI.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		 textUI.GetComponent<SpriteRenderer>().enabled = false;
	}
}
