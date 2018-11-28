using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject[] enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            StartCoroutine(delaySpawn());
        }
        
    }

    IEnumerator delaySpawn(){
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].SetActive(true);
        }
    }
}
