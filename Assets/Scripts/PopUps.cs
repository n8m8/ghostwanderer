using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUps : MonoBehaviour {

	private GameObject textUI;
	void Start(){ 
		textUI = GameObject.FindGameObjectWithTag("Interact");
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			 textUI.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		 textUI.SetActive(false);
	}
}
