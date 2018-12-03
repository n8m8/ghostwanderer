using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingWallDecoration : MonoBehaviour {

    [SerializeField] private bool useAutoSort = true;
    [SerializeField] private int off_set = 10;

	// Use this for initialization
	void Start () {
		if(useAutoSort)
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                transform.parent.GetComponent<SpriteRenderer>().sortingOrder + off_set;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
