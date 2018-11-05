using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingWalls : MonoBehaviour {

    [SerializeField] private bool useAutoSort = true;
    [SerializeField] private int sortingOffset = 0;
    [SerializeField] private float sortingScale = 100;


	// Use this for initialization
	void Start () {

        if (useAutoSort)
        {
            GetComponent<SpriteRenderer>().sortingOrder =
                2 * (int)(-1 * transform.position.y * sortingScale) + sortingOffset;
        }
    }

    public void update_sorting(int offset)
    {
        GetComponent<SpriteRenderer>().sortingOrder =
                2 * (int)(-1 * transform.position.y * sortingScale) + offset;
    }
}
