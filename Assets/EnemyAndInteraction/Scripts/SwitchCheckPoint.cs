using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCheckPoint : MonoBehaviour {
    public Transform checkPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<CheckPointManager>().checkPoint = checkPoint;
        }
    }
}
