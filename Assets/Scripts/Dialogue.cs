using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float typingSpeed;
	public GameObject continueButton;
	public string name; 
	public GameObject image;
	private bool checkflag = false;

	void Start(){
		continueButton.SetActive(false);
		image.SetActive(false);
	}

	void Update(){

		//start the dialogue
		if (Input.GetKeyDown(KeyCode.Space) && checkflag){
			StartCoroutine(Type());
			GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			checkflag = false;
		}

		//load the continue dialogue button
		if (textDisplay.text == (name + sentences[index])){
			continueButton.SetActive(true);
		}

		//call next sentence method upon completely loaded sentence and key press
		if (Input.GetKeyDown(KeyCode.Space) && textDisplay.text == (name + sentences[index]) && !checkflag){
			NextSentence();
		}

		//skip the dialogue after keypress 
		if (Input.GetKeyDown(KeyCode.Escape) && !checkflag){
			resetDialogue();
		}
	}


	//Load the text in one by one
	IEnumerator Type(){
		image.SetActive(true);
		//check for animation type here and execute it maybe enums?? 
		textDisplay.text += name;
		foreach(char letter in sentences[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

	}

	//load the next sentence
	public void NextSentence(){
		if (index < sentences.Length - 1){
			index++;
			textDisplay.text = "";
			StartCoroutine(Type());
			continueButton.SetActive(false);
		}
		else{
			resetDialogue();
		}
	}

	//checkflag is true if player
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			checkflag = true;
		}
	}

	//Reset the checkflag 
	void OnTriggerExit2D(Collider2D collider){
		checkflag = false;
	}

	//reset the dialogue box 
	public void resetDialogue(){
		image.SetActive(false);
		textDisplay.text = "";
		continueButton.SetActive(false);
		index = 0;
		GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		checkflag = true;
	}



}
