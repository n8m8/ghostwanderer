using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBoundarySoundPlayer : MonoBehaviour {
	[SerializeField] private GameObject player;
	private TestPlayerMove playerScript;
	private AudioSource source;

	private bool soundPlayed;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		playerScript = player.GetComponent<TestPlayerMove> ();
		soundPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript.isGhost && (Vector3.Magnitude (playerScript.centerPt - player.transform.position) > (playerScript.radius - 0.1f)) && !soundPlayed) {
			soundPlayed = true;
			source.Play ();
		}
		if (playerScript.isGhost && !(Vector3.Magnitude (playerScript.centerPt - player.transform.position) > (playerScript.radius - 0.1f))) {
			soundPlayed = false;
		}
			
	}
}
