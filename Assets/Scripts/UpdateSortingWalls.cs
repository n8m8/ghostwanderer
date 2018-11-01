using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingWalls : MonoBehaviour {

    [SerializeField] private bool useAutoSort = true;
    [SerializeField] private int sortingOffset = -1000;
    [SerializeField] private float sortingScale = 10;


	// Use this for initialization
	void Start () {
        if(useAutoSort)
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                2 * (int)(-1 * transform.position.y * sortingScale) + sortingOffset;
        }
        if (tag == "OnTable")
        {
            GetComponent<SpriteRenderer>().sortingOrder += 100;
        }

    }
}
