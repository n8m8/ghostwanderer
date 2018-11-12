using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUps : MonoBehaviour {

	public TextMeshProUGUI textUI;
	void Start(){
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			 textUI.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		 textUI.enabled = false;
	}
}
