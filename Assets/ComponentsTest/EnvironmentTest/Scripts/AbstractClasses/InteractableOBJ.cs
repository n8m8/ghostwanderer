using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableOBJ : MonoBehaviour {

    public bool trigger1enable { get; set; }
    public bool trigger2enable { get; set; }

    virtual public void trigger1()
    {
        GetComponent<Animator>().SetTrigger("Trigger1");
    }
    virtual public void trigger2()
    {
        GetComponent<Animator>().SetTrigger("Trigger2");
    }


}
