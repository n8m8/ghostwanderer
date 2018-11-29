using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNext : MonoBehaviour {
    public int SceneNumber;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            reportProgress();
            SceneManager.LoadScene(SceneNumber);
        }
    }

    private void reportProgress(){
        GameObject.FindWithTag("Evidences").GetComponent<ProgressTracker>().reachLevel2 = true;
    }
}
