using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpritePhase : MonoBehaviour {

    public Sprite HumanSprite;
    public Sprite GhostSprite;

    //private LevelController LC_script;
    //private PlayerController PC_script;

    private TestPlayerMove TPM_script;

    private bool current_is_ghost;

	// Use this for initialization
	void Start () {
        this.current_is_ghost = false;
        this.TPM_script = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
    }
	
	// Update is called once per frame
	void Update () {
        if (TPM_script.isGhost != current_is_ghost)
        {
            current_is_ghost = TPM_script.isGhost;
            toggle_sprite();
        }
    }

    private void toggle_sprite()
    {
        if(current_is_ghost)
        {
            if(gameObject.tag == "FloorParent")
            {
                foreach (Transform child in transform)
                {
                    try
                    {
                        child.GetComponent<TwoPhaseSprites>().to_ghost_sprite();
                    }
                    catch { Debug.Log("toggle_sprite " + gameObject.name); }
                }
            }
            else
            {
                try
                {
                    GetComponent<SpriteRenderer>().sprite = GhostSprite;
                }
                catch { Debug.Log("toggle_sprite " + gameObject.name); }
            }
            if (gameObject.tag == "HighWall")
            {
                foreach(Transform child in transform)
                {
                    try
                    {
                        child.GetComponent<TwoPhaseSprites>().to_ghost_sprite();
                    }
                    catch { Debug.Log("toggle_sprite " + gameObject.name); }
                }
            }
        }
        else
        {
            if (gameObject.tag == "FloorParent")
            {
                foreach (Transform child in transform)
                {
                    try
                    {
                        child.GetComponent<TwoPhaseSprites>().to_human_sprite();
                    }
                    catch { Debug.Log("toggle_sprite " + gameObject.name); }
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = HumanSprite;
            }
            if (gameObject.tag == "HighWall")
            {
                foreach (Transform child in transform)
                {
                    try
                    {
                        child.GetComponent<TwoPhaseSprites>().to_human_sprite();
                    }
                    catch { Debug.Log("toggle_sprite " + gameObject.name); }
                }
            }
        }
    }
}
