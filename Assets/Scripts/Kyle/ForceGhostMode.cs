﻿using System.Collections;
using UnityEngine;

public class ForceGhostMode : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject scientist;

    private TestPlayerMove ghostScript;
    private float originalSpeed;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == player.tag && !ghostScript.isGhost)
        {
            scientist.SetActive(true);
            //disable input
            ghostScript.DisableMovement();
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator PlayAnimation(){
        yield return new WaitForSeconds(5f);
        player.GetComponent<Animator>().SetBool("isGhost", true);
        yield return new WaitForSeconds(0f);
        ghostScript.EnableMovement();
		//sets player's location to chest
		ghostScript.EnableGhostMode(new Vector3(3.208198f,-3.886554f,0f)); 
        Destroy(GameObject.Find("TestPlayer(Clone)"));
        this.GetComponent<EdgeCollider2D>().enabled = false;
        ghostScript.ChangeRadius(7f);
        //Camera.main.transform.position = destination.transform.position + new Vector3(0, 0, -20);
    }


}
