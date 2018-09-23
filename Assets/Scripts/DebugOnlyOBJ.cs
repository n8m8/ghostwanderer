using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnlyOBJ : MonoBehaviour {

    // Use this for initialization

    void Start () {
        GetComponent<MeshRenderer>().enabled = false;

#if UNITY_EDITOR
        GetComponent<MeshRenderer>().enabled = true;
#endif


    }


    // Update is called once per frame
    void Update () {
		
	}
}
