﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && Input.GetKeyDown ("e")) {
			GetComponent<Animator> ().SetTrigger ("Open");
			GetComponent<PopUps> ().hidden = true;
		}
	}

}
