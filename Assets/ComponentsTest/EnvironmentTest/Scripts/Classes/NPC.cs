using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public bool isGhost = false;
    public bool seenInRealWorld = false;
    public bool seenInSpiritWorld = false;

    public bool isInvulnerable = false;

    public Weapon weapon;
    public int health;

    //public PatrolAI patralAI;

    public int currentSpeakingVolume;


	// Use this for initialization
	void Start () {

        currentSpeakingVolume = 20;
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Dialogue interaction the same as interactable objects
    public void DialogeInteraction()
    {

    }
}
