using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjects : MonoBehaviour {

    //NEEDS A COLLIDER
    //NEEDS MOVABLE
    //True if the object is locked
    [SerializeField] public bool isLocked;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        if (hit.name.Contains("Chair"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
        else if (hit.name.Contains("Chair"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
    }
}
