using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCircle : MonoBehaviour {

	private GameObject player;
	private TestPlayerMove playerMoveScript;
	private bool wasGhost;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMoveScript = player.GetComponent<TestPlayerMove> ();
		wasGhost = false;
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerMoveScript.isGhost && !wasGhost) {
			float radius = playerMoveScript.radius;
			transform.localScale = new Vector3 (radius, radius, 1);
			transform.position = player.transform.position + new Vector3(0,0.8f);
			GetComponent<SpriteRenderer> ().enabled = true;
			wasGhost = true;
		}

		if (!playerMoveScript.isGhost && wasGhost) {
			GetComponent<SpriteRenderer> ().enabled = false;
			wasGhost = false;
		}
	}
}
