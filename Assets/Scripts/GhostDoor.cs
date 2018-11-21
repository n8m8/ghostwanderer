using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDoor : MonoBehaviour {

    [SerializeField] private TestPlayerMove TPM_script;
    [SerializeField] private DoorTrigger DT_script;

    [SerializeField] private bool is_ghostdoor = true;

    private bool last_phase_human;
    private bool color_changed;
    private Color init_color;

	// Use this for initialization
	void Start () {
        this.color_changed = false;
        this.last_phase_human = false;
        this.init_color = GetComponent<SpriteRenderer>().color;

    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.E) && DT_script.in_locked_trigger && TPM_script.isGhost)
        {
            DT_script.isLocked = false;
            GetComponent<SpriteRenderer>().color = init_color;
        }

        if(last_phase_human && TPM_script.isGhost)
        {
            if(DT_script.isLocked)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
            last_phase_human = false;
        }
        else if(!last_phase_human && !TPM_script.isGhost)
        {
            GetComponent<SpriteRenderer>().color = init_color;
            last_phase_human = true;
        }


	}
}
