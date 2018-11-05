using System.Collections;
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
            try
            {
                if (child.tag == "SpawnByState")
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = 
                                        child.GetComponent<UpdateRender>().Sprite_on;
                }
                else if (child.name == "Vase")
                {
                    child.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            catch { Debug.Log("child sprite render on " + child.name); }
        }
    }

    public void turn_off_cobjs()
    {
        foreach (Transform child in transform)
        {
            try
            {
                if(child.name == "Vase")
                {
                    child.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
            catch{ Debug.Log("child sprite render off" + child.name); }
            
        }
    }
}
