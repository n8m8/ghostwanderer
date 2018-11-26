using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	public void levelOne(){
		SceneManager.LoadScene("FirstLevelTransition");
	}

	public void levelThree(){
		SceneManager.LoadScene("SecondLevel");
	}
}
