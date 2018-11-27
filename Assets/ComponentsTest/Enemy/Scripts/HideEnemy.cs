using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnemy : MonoBehaviour {
    public GameObject[] enemy;
    private bool isIn;

	// Use this for initialization
	void Start () {
        isIn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (enemy[0].activeSelf)
        {
            if (isIn)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    enemy[i].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isIn = false;
        }
    }
}
