using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAnmination : MonoBehaviour {
    public GameObject lightb;
    public GameObject trigger;
    private SpriteRenderer sprite;
    private PossessController possess;
    private Color normal;

	// Use this for initialization
	void Start () {
        sprite = lightb.GetComponent<SpriteRenderer>();
        possess = trigger.gameObject.GetComponent<PossessController>();
        normal = sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (possess.isPossessing)
        {
            StartCoroutine(flash());
        }
        else{

        }
	}

    IEnumerator flash (){
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        sprite.color = normal;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        sprite.color = normal;
    }
}
