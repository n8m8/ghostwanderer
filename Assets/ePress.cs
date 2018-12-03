using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ePress : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(Type());
			GetComponent<Animator>().SetBool("ePress", false);
		}
	}

		//Load the text in one by one
    IEnumerator Type(){
		GetComponent<Animator>().SetBool("ePress", true);
		yield return new WaitForSeconds(2);

	}
}
