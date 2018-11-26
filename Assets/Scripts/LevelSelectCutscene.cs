using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectCutscene : MonoBehaviour {

	public TextMeshProUGUI textDisplay;
	public string[] sentences;
	private int index;
	public float typingSpeed;
	public GameObject continueButton;

	void Start(){
		continueButton.SetActive(false);
		StartCoroutine(Type());
	}

	void Update(){

		//load the continue dialogue button
		if (textDisplay.text == (sentences[index])){
			continueButton.SetActive(true);
		}

		//skip the dialogue after keypress 
		if (Input.GetKeyDown(KeyCode.Space) && textDisplay.text == (sentences[index])){
			SceneManager.LoadScene("FirstLevel");
		}
	}


    //Load the text in one by one
    IEnumerator Type(){
		//check for animation type here and execute it maybe enums?? 
		foreach(char letter in sentences[index].ToCharArray()){
			textDisplay.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

	}


}
