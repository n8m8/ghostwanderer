using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn_on_ccover()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void turn_off_ccover()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
