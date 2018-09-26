using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : InteractableOBJ
{
    public enum SwitchStateEnum { On,Off}

    public GameObject ConnectedGO;
    public SwitchStateEnum SwitchState = SwitchStateEnum.Off;

    private Color origin_color;
    private Animator switchAnimator;

	// Use this for initialization
	void Start () {
        this.origin_color = ConnectedGO.GetComponent<Renderer>().material.color;
        this.switchAnimator = GetComponent<Animator>();

        init_animator();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void init_animator()
    {
        switch(SwitchState)
        {
            case SwitchStateEnum.On:
                switchAnimator.SetTrigger("SwitchOn");
                break;
            case SwitchStateEnum.Off:
                switchAnimator.SetTrigger("SwitchOff");
                break;
        }
    }

    public void switchOn()
    {
        ConnectedGO.GetComponent<Renderer>().material.color = Color.green;
    }

    public void switchOff()
    {
        ConnectedGO.GetComponent<Renderer>().material.color = origin_color;
    }



}
