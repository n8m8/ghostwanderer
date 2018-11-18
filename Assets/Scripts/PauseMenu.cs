using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;
	public GameObject pauseMenu;
	public GameObject pauseNotification;

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
		pauseNotification.SetActive(true);
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	public void Pause(){
		pauseNotification.SetActive(false);
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
