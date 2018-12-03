using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;
	public GameObject pauseMenu;
	public GameObject pauseNotification;
	private Image pauseImage;

	void Start(){
		pauseImage = pauseNotification.GetComponent<Image>();
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
		pauseImage.enabled = true;
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause(){
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
