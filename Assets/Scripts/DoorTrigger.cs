using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject door;
    [SerializeField]
    public bool isLocked = false;
    private Inventory inventory;
    // Use this for initialization
    private DoorController DC_script;

    private bool door_animated = true;

    void Start()
    {
        this.DC_script = door.GetComponent<DoorController>();
        if(DC_script == null)
        {
            door_animated = false;
        }
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D PC = door.GetComponent<Collider2D>();

        if (isLocked)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i] == "key")
                {
                    inventory.removeItem(i);
                    PC.isTrigger = true;
                    isLocked = false;
                    if (door_animated) { DC_script.open_door(); }
                    else { door.GetComponent<Renderer>().enabled = false; }
					AudioSource creak = door.GetComponent<AudioSource> ();
					creak.Play ();
                    
                }
            }
        }
        else
        {
            PC.isTrigger = true;
            if (door_animated) { DC_script.open_door(); }
            else { door.GetComponent<Renderer>().enabled = false; }
			AudioSource creak = door.GetComponent<AudioSource> ();
			creak.Play ();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D PC = door.GetComponent<Collider2D>();
        PC.isTrigger = false;

        if (door_animated)
        {
            if (!isLocked)
            {
                DC_script.close_door();
            }
        }
        else { door.GetComponent<Renderer>().enabled = true; }
    }
}