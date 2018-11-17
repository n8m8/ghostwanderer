using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpritePhase : MonoBehaviour {

    public Sprite HumanSprite;
    public Sprite GhostSprite;

    //private LevelController LC_script;
    //private PlayerController PC_script;

    private TestPlayerMove TPM_script;

    private bool current_is_sprite;

	// Use this for initialization
	void Start () {
        this.current_is_sprite = false;
        //this.LC_script = GameObject.Find("Level Controller").GetComponent<LevelController>();
        //this.PC_script = GameObject.Find("TestPlayer").GetComponent<PlayerController>();
        this.TPM_script = GameObject.Find("TestPlayer").GetComponent<TestPlayerMove>();
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("PC_script.playerStatus.isSpirit " + PC_script.playerStatus.isSpirit);
        //Debug.Log("LC_script.playerIsGhost " + LC_script.playerIsGhost);
        //Debug.Log("TPM_script.isGhost " + TPM_script.isGhost);

        if (TPM_script.isGhost != current_is_sprite)
        {
            current_is_sprite = TPM_script.isGhost;
            toggle_sprite();
        }


    }

    private void toggle_sprite()
    {
        if(current_is_sprite)
        {
            GetComponent<SpriteRenderer>().sprite = GhostSprite;
            if(gameObject.tag == "HighWall")
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
            GetComponent<SpriteRenderer>().sprite = HumanSprite;
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
