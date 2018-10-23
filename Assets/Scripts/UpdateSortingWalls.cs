using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingWalls : MonoBehaviour {

    public bool UseAutoSort = true;
    public int SortingOffset = -1000;
    public float SortingScale = 10;

	// Use this for initialization
	void Start () {
        if(UseAutoSort)
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                2 * (int)(-1 * transform.position.y * SortingScale) + SortingOffset;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
