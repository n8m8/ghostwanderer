using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour {

	[SerializeField] private GameObject buttomImage;
	private TestPlayerMove playerScript;
	[SerializeField] private bool ghostOnly;
	[SerializeField] private bool humanOnly;
	[SerializeField] private bool repeatable;
	[SerializeField] private Vector3 relativeLocation = new Vector3(0,1,0);
	[SerializeField] private string actionKey = "e";

	public bool hidden = false;
	private GameObject popup;
	private bool doneOnce;

	void Start(){
		playerScript = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
		doneOnce = false;
	}

	void Update(){
		if (popup != null) {
			
			//if action can only be done once, removes popup once its done
			if (Input.GetKeyDown (actionKey)) {
				if (!repeatable)
					Destroy (popup);
				doneOnce = true;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag=="Player"){
		//ghost mode prompts only appear once you unlock ghost mode
			if (!actionKey.Equals ("q") || playerScript.ghostAvailable) { 
				if ((!ghostOnly || playerScript.isGhost) && (!humanOnly || !playerScript.isGhost) && (repeatable || !doneOnce))
					popup = (GameObject)Instantiate (buttomImage, transform.position + relativeLocation, new Quaternion ());
			} 
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag=="Player")
			Destroy(popup);
	}
}
