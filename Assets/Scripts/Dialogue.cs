﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour {

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	private float typingSpeed = .01f;
	public GameObject continueButton;
	public string name; 
	public GameObject image;
	private RawImage dialogueBox;
	private bool checkflag = false;
    //True if the player is in range to pick up and item 
    private bool itemZone = false;
    public bool inProgress = false;
	private TestPlayerMove playerScript;

	void Start(){
		continueButton.SetActive(false);
		image.SetActive(false);
		dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<RawImage>();
		playerScript = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
		dialogueBox.enabled = false;
	}

	void Update(){

		//start the dialogue
		if (Input.GetKeyDown(KeyCode.E) && checkflag){
			playerScript.ghostAvailable = false;
			StartCoroutine(Type());
			//GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>().enabled = false;
			GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			checkflag = false;
            inProgress = true;
            Pickup.inProgress = true;
		}

		//load the continue dialogue button
		if (textDisplay.text == (name + sentences[index])){
			continueButton.SetActive(true);
		}

		//call next sentence method upon completely loaded sentence and key press
		if (Input.GetKeyDown(KeyCode.E) && textDisplay.text == (name + sentences[index]) && !checkflag){
			NextSentence();
		}

		//skip the dialogue after keypress 
		if (Input.GetKeyDown(KeyCode.Space) && textDisplay.text == (name + sentences[index]) && !checkflag){
			resetDialogue();
		}
	}


    //Load the text in one by one
    IEnumerator Type(){
		image.SetActive(true);
		dialogueBox.enabled = true;
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
        if(collider.name == "TestPlayer")
        {
            checkflag = false;
        }

    }

	//reset the dialogue box 
	public void resetDialogue(){
		playerScript.ghostAvailable = true;
		image.SetActive(false);
		dialogueBox.enabled = false;
		textDisplay.text = "";
		continueButton.SetActive(false);
		index = 0;
		GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		//GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>().enabled = true;
		checkflag = true;
        inProgress = false;
        Pickup.inProgress = false;
	}



}
