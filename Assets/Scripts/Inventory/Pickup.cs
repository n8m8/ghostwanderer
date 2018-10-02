using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public Inventory inventory;
	private string name;
	private bool display = false;

	// Use this for initialization
	void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")){
			for (int i = 0; i < inventory.slots.Length; i++){
				if (inventory.isFull[i] == false){
					name = gameObject.name;
					inventory.isFull[i] = true;
					display = true;
					inventory.slots[i] = Object.Instantiate(gameObject);
					Destroy(gameObject);
					Debug.Log("Item Picked up");
					break;
				}
			}
		}
	}

	void OnGUI(){
		if (display){
			GUI.Label(new Rect(0,0,100,100),name + "has been picked up");
			float time = 3.0f;
			if ((time -= Time.deltaTime) == 0) 
				display = false;
		}
	}
}
