using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;
	public GameObject pauseMenu;
	public GameObject pauseNotification;
	public TextMeshProUGUI keyCounter;
	public TextMeshProUGUI evidenceCounter;
	private Image pauseImage;
	private TextMeshProUGUI inventoryDisplay;
	private TextMeshProUGUI evidenceDisplay;

	void Start(){
		pauseImage = pauseNotification.GetComponent<Image>();
		inventoryDisplay = GameObject.FindGameObjectWithTag("InventoryPickup").GetComponent<TextMeshProUGUI>();
		evidenceDisplay = GameObject.FindGameObjectWithTag("EvidenceDisplay").GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (gameIsPaused){
				Resume();
			}
			else{
				Pause();
			}
		}
		
	}

	public void Resume(){
		keyCounter.text = "";
		evidenceCounter.text = "";
		pauseImage.enabled = true;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause(){
		keyCounter.text = inventoryDisplay.text;
		evidenceCounter.text = evidenceDisplay.text;
		pauseImage.enabled = false;
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void LoadMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
