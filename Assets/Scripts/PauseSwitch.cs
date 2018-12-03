using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseSwitch : MonoBehaviour {

	public Sprite ghostPause;
	public Sprite humanPause;
	private TestPlayerMove playerScript;
	private Image pauseUI;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
		pauseUI = GameObject.Find("pause").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScript.isGhost){
			pauseUI.sprite = ghostPause;
		}
		else{
			pauseUI.sprite = humanPause;
		}
	}
}
