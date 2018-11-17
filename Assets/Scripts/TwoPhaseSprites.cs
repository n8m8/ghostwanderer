using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPhaseSprites : MonoBehaviour {

    [SerializeField] private Sprite HumanSprite;
    [SerializeField] private Sprite GhostSprite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void to_human_sprite()
    {
        GetComponent<SpriteRenderer>().sprite = HumanSprite;
    }

    public void to_ghost_sprite()
    {
        GetComponent<SpriteRenderer>().sprite = GhostSprite;

    }

}
