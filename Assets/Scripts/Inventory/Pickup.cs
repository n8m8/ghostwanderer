using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour {

	public Inventory inventory;
	private string name;
	public TextMeshProUGUI textDisplay;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}
	
	IEnumerator OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")){
			for (int i = 0; i < inventory.slots.Length; i++){
				if (inventory.isFull[i] == false){
					gameObject.GetComponent<SpriteRenderer>().enabled = false;
					name = gameObject.name;
					inventory.isFull[i] = true;
					inventory.slots[i] = name;
					textDisplay.text = (name + " has been picked up");
					yield return new WaitForSeconds(3);
					textDisplay.text = "";
					//will be buggy if player goes back in 
					Destroy(gameObject);
					break;
				}
			}
		}
	}
}
