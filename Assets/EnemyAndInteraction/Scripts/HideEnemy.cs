using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideEnemy : MonoBehaviour {
    public GameObject[] enemy;
    private GameObject player;
    private TestPlayerMove control;
    private bool isIn;

	// Use this for initialization
	void Start () {
        isIn = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            control = player.gameObject.GetComponent<TestPlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
            if (isIn)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    if (enemy[i] != null)
                    {
                        if (enemy[i].activeSelf)
                        {
                            enemy[i].GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                }
            }
            else if(isIn == false && control != null && control.isPossessing == false)
            {
                for (int i = 0; i < enemy.Length; i++)
                {
                    if (enemy[i] != null)
                    {
                        if (enemy[i].activeSelf)
                        {
                            enemy[i].GetComponent<SpriteRenderer>().enabled = false;
                        }
                    }
                }
            }
            else{

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
