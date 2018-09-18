using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    //True if a melee weapon
    [SerializeField] public bool melee = false;
    //weapon damage
    [SerializeField] public int damage = 0;
    //weapon range
    [SerializeField] public int range = 0;
    //Rate of fire of weapon
    [SerializeField] public int rate = 0;
    //Last time the weapon was fired
    [SerializeField] public int lastFired = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
