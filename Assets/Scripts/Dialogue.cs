using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {

	//NAMES, ARTWORK PIXEL CHARACTER BOXES??

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float typingSpeed;
	public GameObject continueButton;

	void Start(){
		// for testing purposes
		StartCoroutine(Type());
	}

	void Update(){
		//input k as nextsentence
		//escape to quit
		// 		if (Input.GetButtonDown("Skip")){
		// 			StopAllCoroutines();
		// 		}
		// if (Input.GetButtonDown("K")){
		// 	NextSentence();
		// }

		if (textDisplay.text == sentences[index]){
			continueButton.SetActive(true);
		}
	}

	IEnumerator Type(){
		//check for animation type here and execute it maybe enums?? 
		foreach(char letter in sentences[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

	}

	public void NextSentence(){
		if (index < sentences.Length - 1){
			index++;
			textDisplay.text = "";
			StartCoroutine(Type());
			continueButton.SetActive(false);
		}
		else{
			textDisplay.text = "";
			continueButton.SetActive(false);
		}
	}


// have to freeze character on entrance and mayb animate to turn towards, zoom in screen?
// 	void OnTriggerEnter2D(Collider2D collider){
// 		if (collider.tag == "Player" && Input.GetButtonDown("Talk")){
			// StartCoroutine(Type());
// 		}
// 	}



}
