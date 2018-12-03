using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour {
	[SerializeField] private float intervalMin;
	[SerializeField] private float intervalMultiplier;
	private float nextToggle;
	private float timeCounter;

	void Start() {
		nextToggle = -1;
		timeCounter = 0;
	}

	// Use this for initialization
	void OnTriggerStay2D(Collider2D other)
	{
		timeCounter += Time.deltaTime;
		if (timeCounter > nextToggle){
			GetComponent<SpriteRenderer> ().enabled = !GetComponent<SpriteRenderer> ().enabled;
			timeCounter = 0;
			nextToggle = (Random.value * intervalMultiplier) + 0.1f;
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		GetComponent<SpriteRenderer> ().enabled = false;
	}
}
