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
	public string name; 
	public GameObject image;

	void Start(){
		// // for testing purposes
		// StartCoroutine(Type());
		continueButton.SetActive(false);
		image.SetActive(false);
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

		if (textDisplay.text == (name + sentences[index])){
			continueButton.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.Space) && textDisplay.text == (name + sentences[index])){
			NextSentence();
		}
	}


	IEnumerator Type(){
		image.SetActive(true);
		//check for animation type here and execute it maybe enums?? 
		textDisplay.text += name;
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
			image.SetActive(false);
			textDisplay.text = "";
			continueButton.SetActive(false);
		}
	}


//have to freeze character on entrance and mayb animate to turn towards, zoom in screen?
	//Input.GetKeyDown(KeyCode.Space)
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			//Freeze the character xd
			StartCoroutine(Type());
		}
	}

	// //reset everyhing
	// void OnTriggerExit2D(Collider2D collider){
	
	// }



}
