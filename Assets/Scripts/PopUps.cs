using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour {

	private GameObject textUI;
	void Start(){ 
		textUI = GameObject.FindGameObjectWithTag("Interact");
		textUI.GetComponent<RawImage>().enabled= false;
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			 textUI.GetComponent<RawImage>().enabled= true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		 textUI.GetComponent<RawImage>().enabled= false;
	}
}
