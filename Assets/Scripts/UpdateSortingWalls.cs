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
            int new_order = 
                        2 * (int)(-1 * transform.position.y * sortingScale) + sortingOffset;
            GetComponent<SpriteRenderer>().sortingOrder = new_order;

            if(gameObject.tag == "HighWall")
            {
                foreach (Transform child in transform)
                {
                    try
                    {
                        child.GetComponent<SpriteRenderer>().sortingOrder = new_order + 10;
                    }
                    catch { Debug.Log("auto sort " + gameObject.name); }
                }
            }
        }
    }

    public void update_sorting(int offset)
    {
        GetComponent<SpriteRenderer>().sortingOrder =
                2 * (int)(-1 * transform.position.y * sortingScale) + offset;
    }
}
