using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundFX : MonoBehaviour {
	[SerializeField] private GameObject player;
	[SerializeField] private AudioClip humanSteps;
	[SerializeField] private AudioClip ghostSteps;
	private TestPlayerMove playerScript;
	private AudioSource source;

	private bool started;
	private bool wasGhost;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		playerScript = player.GetComponent<TestPlayerMove> ();
		started = false;
		wasGhost = playerScript.isGhost;
	}


	// Update is called once per frame
	void Update () {
		if (Vector3.Magnitude (new Vector3 (playerScript.getMovement ().x, playerScript.getMovement ().y)) > 0.01) {
			if (!started || wasGhost != playerScript.isGhost) {
				if (playerScript.isGhost)
					source.clip = ghostSteps;
				else
					source.clip = humanSteps;
				started = true;
				source.Play ();
			}
		} else {
			started = false;
			source.Stop ();
		}
		wasGhost = playerScript.isGhost;
	}
}
