using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPhase : MonoBehaviour {

    private LevelController LC_script;
    private TwoPhasedOBJ TP_OBJ;

    private bool triggered = false;

	// Use this for initialization
	void Start () {
        LC_script = GameObject.Find("LevelController").GetComponent<LevelController>();
        TP_OBJ = GetComponent<TwoPhasedOBJ>();
    }
	
	// Update is called once per frame
	void Update () {
        check_phase();
        //Debug.Log("wall " + TP_OBJ.LocalPhaseState);
        //Debug.Log("wall " + GetComponent<Renderer>().material.color);
    }

    public void check_phase()
    {
        if(TP_OBJ.LocalPhaseState != LC_script.PhaseState)
        {
            switch(LC_script.PhaseState)
            {
                case LevelController.PhaseStateEnum.human:
                    TP_OBJ.active_phase1();
                    break;
                case LevelController.PhaseStateEnum.ghost:
                    TP_OBJ.active_phase2();
                    break;
            }
        }
    }
}
