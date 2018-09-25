using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour {
    //The trigger for this object
    [SerializeField] private int trigger;
    //The response when triggered
    [SerializeField] private string act;

    //TODO change the above to usable classes


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        //If kill is set as an input value kill the player
        if (hit.name.Contains("TestPlayer") && act.Equals("Kill"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
        //If disappear is set as an input value destroy this object
        else if (hit.name.Contains("TestPlayer") && act.Equals("Disappear"))
        {
            Destroy(hit);
            yield return new WaitForSeconds(1);
        }
    }
}
