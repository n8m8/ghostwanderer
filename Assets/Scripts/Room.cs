using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    //Number of the level and floor the room is in
    public int RoomNumber;
    public int Level;
    public int Floor;
    public bool isDiscovered;

    public WallParent WallParent1;
    public WallParent WallParent2;
    public WallParent WallParent3;
    public WallParent WallParent4;
    public ObjectParent ObjectParent1;

    //True if the room has been entered
    ////Array list of environment objects in the room
    //[SerializeField] public ArrayList envObjects;
    ////Array list of interactable objects in the room 
    //[SerializeField] public ArrayList interactableObjects;



    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turn_off_walls()
    {
        WallParent1.turn_off_cwalls();
        WallParent2.turn_off_cwalls();
        WallParent3.turn_off_cwalls();
        WallParent4.turn_off_cwalls();
        ObjectParent1.turn_off_cobjs();
    }

    public void turn_on_walls()
    {
        WallParent1.turn_on_cwalls();
        WallParent2.turn_on_cwalls();
        WallParent3.turn_on_cwalls();
        WallParent4.turn_on_cwalls();
        ObjectParent1.turn_on_cobjs();
    }
}
