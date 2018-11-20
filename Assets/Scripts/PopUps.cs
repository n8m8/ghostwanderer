using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUps : MonoBehaviour {

	public GameObject buttomImage;
	private TestPlayerMove playerScript;
	[SerializeField] bool ghostOnly;
	public bool hidden = false;
	private GameObject popup;

	void Start(){
		playerScript = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
	}
	void OnTriggerEnter2D(Collider2D collider){
		if(!ghostOnly || playerScript.isGhost)
			popup = (GameObject) Instantiate(buttomImage, transform.position+ new Vector3(0,1f,0), transform.rotation);
	}

	void OnTriggerExit2D(Collider2D collider){
		 //buttomImage.GetComponent<RawImage>().enabled= false;
		 Destroy(popup);
	}
}
