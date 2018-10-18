using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private Animator DCAnimator;

	// Use this for initialization
	void Start () {
        this.DCAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void open_door()
    {
        DCAnimator.SetTrigger("OpenDoor");
    }

    public void close_door()
    {
        DCAnimator.SetTrigger("CloseDoor");

    }
}
