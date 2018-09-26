using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSortingConstant : MonoBehaviour {

    public float sorting_scale = 100;
    public Transform StandIndicator;

    private Vector3 stand_pos;
    private float sprite_height;
    private float sprite_width;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.sprite_height = spriteRenderer.bounds.size.y;
        this.sprite_width = spriteRenderer.bounds.size.x;
        
    }
	
	// Update is called once per frame
	void Update () {
        stand_pos = StandIndicator.transform.position;
        spriteRenderer.sortingOrder = (int)(-1 * sorting_scale * stand_pos.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(stand_pos - (Vector3.left * sprite_width / 2.0f),
                            stand_pos + (Vector3.left * sprite_width / 2.0f));
    }
}
