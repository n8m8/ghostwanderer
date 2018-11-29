using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour {
    public string levelname;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene(levelname);
    }

    public void backToMenu(){
        SceneManager.LoadScene("MainMenu");
    }


}
