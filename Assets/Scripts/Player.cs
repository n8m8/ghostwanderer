using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Health out of 100
    [SerializeField] public int health;

    [SerializeField] public Weapon weapon;
    // Use this for initialization
    void Start () {
        health = 100;
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
        else if (hit.name.Contains("Table"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
    }
}
