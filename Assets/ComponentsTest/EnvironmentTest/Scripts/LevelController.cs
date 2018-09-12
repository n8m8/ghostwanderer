using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Just for test pruposes, will not cosider any structure purposes;
public class LevelController : MonoBehaviour {

    public enum PhaseStateEnum { human,ghost}

    public PhaseStateEnum PhaseState { get; set; }

    //change player status from levelcontroller, obviously not a good solution, just convinient for test;
    public GameObject player;

	// Use this for initialization
	void Start () {
        this.PhaseState = PhaseStateEnum.human;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("levelcontroller " + PhaseState);
	}

    public void toggle_phase()
    {
        switch(PhaseState)
        {
            case PhaseStateEnum.human:
                PhaseState = PhaseStateEnum.ghost;
                player.layer = LayerMask.NameToLayer("Ghost");
                break;
            case PhaseStateEnum.ghost:
                PhaseState = PhaseStateEnum.human;
                player.layer = LayerMask.NameToLayer("Default");
                break;
        }

    }
}
