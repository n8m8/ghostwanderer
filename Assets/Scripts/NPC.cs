using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    //True if the enemy is a ghost
    [SerializeField] public bool isGhost = false;
    //True if the enemy is visible in the real world
    [SerializeField] public bool seenInRealWorld = false;
    //true if the enemy is visible in the spirit world
    [SerializeField] public bool seenInSpiritWorld = false;
    //boolean for if the enemy is invulnerable
    [SerializeField] public bool isInvulnerable = false;
    //Weapon the enemy is holding/using
    [SerializeField] public Weapon weapon;
    //Enemy health out of 100
    [SerializeField] public int health;

    //public PatrolAI patralAI;
    //How far away a speaking dialogue can be seen
    [SerializeField] public int currentSpeakingVolume;


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
