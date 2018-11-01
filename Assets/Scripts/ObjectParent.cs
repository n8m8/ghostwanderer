﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParent : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn_on_cobjs()
    {
        foreach (Transform child in transform)
        {
            if(child.tag == "SpawnByState")
            {
                child.gameObject.GetComponent<SpriteRenderer>().enabled = child.GetComponent<UpdateRender>().Sprite_on;
            }
            else
            {
                child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void turn_off_cobjs()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
