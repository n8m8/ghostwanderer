using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TwoPhasedOBJ : MonoBehaviour {

    public LevelController.PhaseStateEnum LocalPhaseState { get; set; }

    virtual public void active_phase1()
    {
        LocalPhaseState = LevelController.PhaseStateEnum.human;

        Color origin_color = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color =
                    new Color(origin_color.r, origin_color.g, origin_color.b, 1.0f);
    }

    virtual public void active_phase2()
    {
        LocalPhaseState = LevelController.PhaseStateEnum.ghost;

        Color origin_color = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color =
                    new Color(origin_color.r, origin_color.g, origin_color.b, 0.1f);
    }

}
