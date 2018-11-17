using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    private List<bool> init_record;

	// Use this for initialization
	void Start () {
        this.init_record = new List<bool>();

        foreach(Transform child in transform)
        {
            if(child.gameObject.activeSelf)
            {
                init_record.Add(true);
            }
            else
            {
                init_record.Add(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn_on_this_wall()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
        for(int i = 0;i<init_record.Count;i++)
        {
            if(init_record[i])
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void turn_off_this_wall()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        for (int i = 0; i < init_record.Count; i++)
        {
            if (init_record[i])
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
