using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRooms : MonoBehaviour {

    public RoomDetection RD_scripts;
    //public ToggleRoomWalls TRW_script;

    private GameObject[] room_GOs;


    // Use this for initialization
    void Start () {
        this.room_GOs = GameObject.FindGameObjectsWithTag("Room");
    }

    // Update is called once per frame
    void Update () {
		if(RD_scripts.Room_changed)
        {
            toggle_walls(RD_scripts.Current_floor, RD_scripts.Current_room);
            toggle_covers(RD_scripts.Current_floor, RD_scripts.Current_room);
            RD_scripts.Room_changed = false;
        }
	}

    private void toggle_walls(int floor, int room_number)
    {
        foreach (GameObject room_GO in room_GOs)
        {
            Room room_script = room_GO.GetComponent<Room>();
            if (room_script.Floor == floor)
            {
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

    private void toggle_covers(int floor, int room_number)
    {
        foreach (GameObject room_GO in room_GOs)
        {
            Room room_script = room_GO.GetComponent<Room>();
            if (room_script.Floor == floor)
            {
                if (room_script.RoomNumber == room_number)
                {
                    room_script.turn_on_cover();
                }
                else
                {
                    room_script.turn_off_cover();
                }
            }
        }
    }
}
