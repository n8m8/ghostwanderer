using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUps : MonoBehaviour {

	public TextMeshProUGUI textDisplay;

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			textDisplay.text = "Press E to talk";
		}
	}

	//Reset the checkflag 
	void OnTriggerExit2D(Collider2D collider){
		textDisplay.text = "";
	}
}
