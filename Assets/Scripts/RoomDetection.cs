using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour {

    public bool Room_changed { get; set; }
    public Room Room_script { get; set; }


    // Use this for initialization
    void Start () {
        this.Room_changed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerStay2D(Collider2D collider)
    //{
    //    if(collider.gameObject.tag == "Room")
    //    {
    //        room_script = collider.gameObject.GetComponent<Room>();
    //        //Debug.Log("Room " + room_script.RoomNumber);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Room")
        {
            Room_script = collider.gameObject.GetComponent<Room>();
            //Debug.Log("Room " + room_script.RoomNumber);
            Room_changed = true;
        }
    }
}
