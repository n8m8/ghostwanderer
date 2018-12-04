using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMInstantiater : MonoBehaviour {
    public GameObject prototype;

	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectWithTag("Evidences") == null){
            Instantiate(prototype);
        }
	}
	
	// Update is called once per frame
    void Update () {
		
	}
}
