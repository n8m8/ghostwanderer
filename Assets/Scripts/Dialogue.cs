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
		//input k as nextsentence
		//escape to quit
		// 		if (Input.GetButtonDown("Skip")){
		// 			StopAllCoroutines();
		// 		}
		// if (Input.GetButtonDown("K")){
		// 	NextSentence();
		// }

		if (Input.GetKeyDown(KeyCode.E) && checkflag){
			StartCoroutine(Type());
			GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			checkflag = false;
		}

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
			index = 0;
			GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			checkflag = true;
		}
	}


	void OnTriggerEnter2D(Collider2D collider){
		if (collider.name == "TestPlayer"){
			//Freeze the character xd
			checkflag = true;
		}
	}

	//reset everyhing
	void OnTriggerExit2D(Collider2D collider){
		checkflag = false;
	}



}
