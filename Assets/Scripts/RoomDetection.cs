using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour {

    //public ToggleRooms TR_script;

    public bool Room_changed { get; set; }
    public int Current_room { get; set; }
    public int Current_floor { get; set; }

    private Room Room_script;
    private string last_room_stringtag;
    private string llast_room_stringtag;


    // Use this for initialization
    void Start () {
        this.Room_changed = false;
        this.last_room_stringtag = "";
        this.llast_room_stringtag = "";
        this.Current_floor = -1;
        this.Current_room = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!Room_changed && collider.gameObject.tag == "Room")
        {
            Room_script = collider.gameObject.GetComponent<Room>();
            if(last_room_stringtag != Room_script.Room_stringtag)
            {
                //Debug.Log("Room_script.Room_stringtag " + Room_script.Room_stringtag);
                llast_room_stringtag = last_room_stringtag;
                last_room_stringtag = Room_script.Room_stringtag;
                Current_room = Room_script.RoomNumber;
                Current_floor = Room_script.Floor;
                Room_changed = true;
            }
            //Debug.Log("Room " + room_script.RoomNumber);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.tag == "Room")
    //    {
    //        Room_script = collider.gameObject.GetComponent<Room>();
    //        //Debug.Log("Room " + room_script.RoomNumber);
    //        Room_changed = true;
    //    }
    //}
}
