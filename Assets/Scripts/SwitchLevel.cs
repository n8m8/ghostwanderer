using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour {

    [SerializeField] private GameObject Target;

    public bool switch_on;

    // Use this for initialization
    void Start() {
        switch_on = false;
    }

    // Update is called once per frame
    void Update() {

    }

    public void toggle_switch()
    {
        if(switch_on)
        {
            turn_off_switch();
        }
        else
        {
            turn_on_switch();
        }
    }

    private void turn_on_switch()
    {
        //placeholder;
        switch_on = true;
        Destroy(Target);
    }

    private void turn_off_switch()
    {
        switch_on = false;
    }
}
