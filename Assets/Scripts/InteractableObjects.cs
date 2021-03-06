﻿using System.Collections;
using UnityEngine;

public class InteractableObjects : MonoBehaviour {
    //The trigger for this object
    [SerializeField] private int trigger;
    //The response when triggered
    [SerializeField] private string act;
    //Dialogue
    [SerializeField] private Dialogue dialogue;
    //the LevelController
    [SerializeField] private LevelController levelController;
    //the Inventory
    [SerializeField] private Inventory inventory;
    
    //TODO change the above to usable classes


	// Use this for initialization
	void Start () {
		levelController = FindObjectOfType<LevelController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        //If kill is set as an input value kill the player
        if (hit.name.Contains("Player") && act.Equals("Kill"))
        {
            levelController.RespawnPlayer();
        }
        //If disappear is set as an input value destroy this object
        else if (hit.name.Contains("Player") && act.Equals("Disappear"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(1);
        }

        //If disappear is set as an input value destroy this object
        else if (hit.name.Contains("Player") && act.Equals("Door"))
        {
            for (int i = 0; i < inventory.slots.Length; i++){
                if (inventory.slots[i] == "key"){
                    inventory.removeItem(i);
                    EdgeCollider2D PC = gameObject.GetComponent<EdgeCollider2D>();
                    PC.enabled = false;
                    gameObject.GetComponent<Renderer>().enabled = false;
                    // yield return new WaitForSeconds(1);
                    // gameObject.GetComponent<Renderer>().enabled = true;
                }
            }

        }
        else if (hit.name.Contains("Player") && act.Equals("Damage"))
        {
            //Possible section if we want objects to be damage-able...
            //This could refer to player health or perhaps the integrity of something like a lock in the game

        }

        else if(hit.name.Contains("Player") && act.Equals("Dialogue"))
        {
            //This section should be for dialogue spawning objects
            //Essentially any objects that will display text for the player
            //dialogue.StartCoroutine(Type());
        }
    }
    //Leave this for testing please
    IEnumerator OnCollisionExit2D(Collision2D other)
    {  
        gameObject.GetComponent<Renderer>().enabled = true;
        EdgeCollider2D PC = gameObject.GetComponent<EdgeCollider2D>();
        PC.isTrigger = false;
        Renderer rend = GetComponent<Renderer>();

        //Set the main Color of the Material to green
        rend.material.shader = Shader.Find("Test Player");
        rend.material.SetColor("Test Player", Color.blue);
        yield return new WaitForSeconds(1);
    }


    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        EdgeCollider2D PC = gameObject.GetComponent<EdgeCollider2D>();
        PC.isTrigger = false;
        yield return new WaitForSeconds(1f);

    }
    

}
