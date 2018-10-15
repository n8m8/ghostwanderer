using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChangeDetector : MonoBehaviour {

    public TestPlayerMove TPM_script;

    public bool Phase_changed { get; set; }
    public bool Last_GA { get; set; }
    public bool Last_BA { get; set; }

    private int counter = 0;

	// Use this for initialization
	void Start () {
        this.Phase_changed = false;
        this.Last_GA = TPM_script.ghostAvailable;
        this.Last_BA = TPM_script.bodyAvailable;
	}
	
	// Update is called once per frame
	void Update () {

        counter++;

        if (Last_GA != TPM_script.ghostAvailable && Last_BA != TPM_script.ghostAvailable)
        {
            Last_GA = TPM_script.ghostAvailable;
            Last_BA = TPM_script.bodyAvailable;
            Phase_changed = true;
            counter = 0;
        }

        if(Phase_changed && counter >= 10)
        {
            Phase_changed = false;
        }
	}
}
