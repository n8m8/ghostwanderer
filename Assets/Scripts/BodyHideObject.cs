using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHideObject : MonoBehaviour {

    [SerializeField] private Sprite NormalSprite;
    [SerializeField] private Sprite ContainBodySprite;

    public bool ContainsBody { get; set; }

    private bool last_body_flag;

	// Use this for initialization
	void Start () {
        this.ContainsBody = false;
        this.last_body_flag = ContainsBody;
	}
	
	// Update is called once per frame
	void Update () {
        update_sprite();
	}

    private void update_sprite()
    {
        if(last_body_flag != ContainsBody)
        {
            if(ContainsBody)
            {
                GetComponent<SpriteRenderer>().sprite = ContainBodySprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = NormalSprite;
            }
            last_body_flag = ContainsBody;
        }
    }
}
