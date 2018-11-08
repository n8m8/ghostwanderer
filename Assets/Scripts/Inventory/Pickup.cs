using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour {

	public Inventory inventory;
	private string name;
	public TextMeshProUGUI textDisplay;

    private bool itemZone = false;
    public static bool inProgress = false;
    // Use this for initialization
    void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		textDisplay = GameObject.FindGameObjectWithTag("ItemPickup").GetComponent<TextMeshProUGUI>();
	}
<<<<<<< HEAD
	
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")){
			for (int i = 0; i < inventory.slots.Length; i++){
				if (inventory.isFull[i] == false){
					gameObject.GetComponent<SpriteRenderer>().enabled = false;
					name = gameObject.name;
					inventory.isFull[i] = true;
					inventory.slots[i] = name;
					int number = int.Parse(textDisplay.text.Split(' ')[1])+1;
					Debug.Log(number);
					textDisplay.text = "x " + number.ToString();
					// yield return new WaitForSeconds(3);
					// textDisplay.text = "";
					//will be buggy if player goes back in 
					Destroy(gameObject);
					break;
				}
=======

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && itemZone && !inProgress)
        {
            StartCoroutine(briefMessage("Found something!", 2));
            StartCoroutine(addToInventory());

        }
        else if (Input.GetKeyDown(KeyCode.R) && !itemZone && !inProgress)
        {
            StartCoroutine(briefMessage("Nothing", 1));
        }
    }
    private Collider2D coll;
    IEnumerator addToInventory () {
		for (int i = 0; i < inventory.slots.Length; i++){
			if (inventory.isFull[i] == false){
				coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				name = coll.gameObject.name;
				inventory.isFull[i] = true;
				inventory.slots[i] = name;
				textDisplay.text = (name + " has been picked up");
				yield return new WaitForSeconds(3);
				textDisplay.text = "";
				//will be buggy if player goes back in 
				Destroy(coll.gameObject);
				break;
>>>>>>> 60d93880ed04ce1c48169f921237510e38567e24
			}
		}
	}

    //checkflag is true if player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "SpawnByState" || collider.tag == "EvidenceTrigger")
        {
            itemZone = true;
            coll = collider;
        }
    }

    //Reset the checkflag 
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "SpawnByState" || collider.tag == "EvidenceTrigger")
        {
            itemZone = false;
        }

    }

    IEnumerator briefMessage(string message, int time)
    {
        if (!inProgress)
        {
            textDisplay.text = message;
            yield return new WaitForSeconds(time);
            textDisplay.text = "";
        }
    }
}
