using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRoomWalls : MonoBehaviour {

    public RoomDetection RMD_script;

    private GameObject[] room_GOs;

    // Use this for initialization
    void Start () {
        this.room_GOs = GameObject.FindGameObjectsWithTag("Room");
	}
	
	// Update is called once per frame
	void Update () {
		if(RMD_script.Room_changed)
        {
            RMD_script.Room_changed = false;
            toggle_walls(RMD_script.Room_script.RoomNumber);
        }
	}

    public void toggle_walls(int room_number)
    {
        foreach (GameObject room_GO in room_GOs)
        {
            Room room_script = room_GO.GetComponent<Room>();
            if (room_script.RoomNumber == room_number)
            {
                room_script.turn_on_walls();
            }
            else
            {
                room_script.turn_off_walls();
            }
        }
    }
}
