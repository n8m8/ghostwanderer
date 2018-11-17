using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParent : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn_on_cwalls()
    {
        foreach(Transform child in transform)
        {
            try
            {
                child.GetComponent<Wall>().turn_on_this_wall();
            }
            catch { Debug.Log("turn_off_cwalls " + gameObject.name); }

        }
    }

    public void turn_off_cwalls()
    {
        foreach (Transform child in transform)
        {
            try
            {
                child.GetComponent<Wall>().turn_off_this_wall();
            }
            catch { Debug.Log("turn_off_cwalls " + gameObject.name); }
        }
    }
}
