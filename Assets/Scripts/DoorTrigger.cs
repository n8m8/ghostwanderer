using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    [SerializeField] public GameObject door;
    [SerializeField] public bool isLocked = false;
    private Inventory inventory;
	// Use this for initialization
	void Start () {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EdgeCollider2D PC = door.GetComponent<EdgeCollider2D>();

        if (isLocked)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i] == "key")
                {
                    inventory.removeItem(i);
                    door.GetComponent<Renderer>().enabled = false;
                    PC.isTrigger = true;
                    isLocked = false;
                }
            }
        }
        else
        {
            door.GetComponent<Renderer>().enabled = false;
            PC.isTrigger = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        door.GetComponent<Renderer>().enabled = true;
        EdgeCollider2D PC = door.GetComponent<EdgeCollider2D>();
        PC.isTrigger = false;
    }
}
