using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableOBJ
{

    public enum DoorStateEnum {open,closed,locked}

    public DoorStateEnum DoorStartState = DoorStateEnum.closed;
    public GameObject DoorCollider;
    public GameObject DoorTrigger;

    private Animator doorAnimator;

	// Use this for initialization
	void Start () {
        this.doorAnimator = GetComponent<Animator>();

        init_animator();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("" + gameObject.name + " " + trigger1enable + " " + trigger2enable);
	}

    private void init_animator()
    {
        switch(DoorStartState)
        {
            case DoorStateEnum.open:
                doorAnimator.SetTrigger("Open");
                break;
            case DoorStateEnum.closed:
                doorAnimator.SetTrigger("Close");
                break;
            case DoorStateEnum.locked:
                doorAnimator.SetTrigger("Lock");
                break;
        }
    }

    //public override void trigger1()
    //{
    //    doorAnimator.SetTrigger("Trigger1");
    //}

    //public override void trigger2()
    //{
    //    doorAnimator.SetTrigger("Trigger2");
    //}

}
