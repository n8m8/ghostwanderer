using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour {
    public string levelname;
    public GameObject frame;
    private Ray ray;
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

    private void OnMouseEnter()
    {
        frame.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        frame.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }





}
