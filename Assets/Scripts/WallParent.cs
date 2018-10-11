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
            child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            child.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public void turn_off_cwalls()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
