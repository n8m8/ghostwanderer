using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject destination;
    [SerializeField]
    private Camera camera;

    private TestPlayerMove ghostScript;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == player.tag && !ghostScript.isGhost)
        {
            player.transform.position = destination.transform.position;
            Camera.main.transform.position = destination.transform.position + new Vector3(0, 0, -20);
        }
    }

}
