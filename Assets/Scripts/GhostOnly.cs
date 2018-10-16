using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOnly : MonoBehaviour {

	private TestPlayerMove script;

	// Use this for initialization
	void Start () {
		script = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!script.isGhost){
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			this.gameObject.GetComponent<Dialogue>().enabled = false;
		}
		else if (script.isGhost){
			this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			this.gameObject.GetComponent<Dialogue>().enabled = true;
		}
	}
}
