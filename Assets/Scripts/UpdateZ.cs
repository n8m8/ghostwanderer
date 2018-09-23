using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateZ : MonoBehaviour {

    public Transform StandIndicator;

    private Vector3 stand_pos;
    private float sprite_height;
    private float sprite_width;

    // Use this for initialization
    void Start () {
        //Debug.Log("StandIndicator.position " + StandIndicator.position);


        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        this.sprite_height = spriteRenderer.bounds.size.y;
        this.sprite_width = spriteRenderer.bounds.size.x;
        this.stand_pos = StandIndicator.transform.position;


        transform.position =
            new Vector3(transform.position.x, transform.position.y, stand_pos.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        //Vector3 stand_pos = transform.position;

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(stand_pos - (Vector3.left * sprite_width / 2.0f),
                            stand_pos + (Vector3.left * sprite_width / 2.0f));
    }
}
