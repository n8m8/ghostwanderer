using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour {

	public Inventory inventory;
	private string name;
	public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI inventoryDisplay;

    private bool itemZone = false;
    private bool muteText = false;
    public static bool inProgress = false;
    // Use this for initialization
    void Start () {
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
		//inventoryDisplay = GameObject.FindGameObjectWithTag("InventoryPickup").GetComponent<TextMeshProUGUI>();
        //textDisplay = GameObject.FindGameObjectWithTag("ItemPickup").GetComponent<TextMeshProUGUI>();
	}
	

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && itemZone && !inProgress)
        {
            StartCoroutine(briefMessage("Found something!", 2));
            StartCoroutine(addToInventory());

        }
        else if (Input.GetKeyDown(KeyCode.E) && !itemZone && !inProgress && !muteText)
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
                int number = int.Parse(textDisplay.text.Split(' ')[1])+1;
                Debug.Log(number);
                inventoryDisplay.text = "x " + number.ToString();
                yield return new WaitForSeconds(3);
                // textDisplay.text = "";
                //will be buggy if player goes back in 
				Destroy(coll.gameObject);
				break;
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
        if(collider.tag == "Possess")
        {
            muteText = true;
        }
    }

    //Reset the checkflag 
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "SpawnByState" || collider.tag == "EvidenceTrigger")
        {
            itemZone = false;
        }
        if (collider.tag == "Possess")
        {
            muteText = false;
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
