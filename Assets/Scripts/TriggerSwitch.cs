using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : MonoBehaviour {

    private Transform switchTRANS;

	// Use this for initialization
	void Start () {
        GetComponent<InputManager>().OnInteractButtonPressed += trigger_switch;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwitchTrigger")
        {
            switchTRANS = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SwitchTrigger")
        {
            switchTRANS = null;
        }
    }

    private void trigger_switch()
    {
        if(switchTRANS != null)
        {
            switchTRANS.GetComponent<SwitchLevel>().toggle_switch();
        }
    }
}
