using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceTracker : MonoBehaviour {
    private EvidenceManager manager;
    public GameObject evi1;
    public GameObject evi2;
    public GameObject evi3;
    public GameObject evi4;
    public GameObject evi5;
    public GameObject evi6;
    public GameObject evi7;
    public GameObject evi8;

	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectWithTag("Evidences") == null){

        }
        else{
            manager = GameObject.FindGameObjectWithTag("Evidences").gameObject.GetComponent<EvidenceManager>();
            LoadEvidences();
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadEvidences(){
        List<int> evidences = manager.evidences;


    }

    bool isInList(int code, List<int> list){
        return list.IndexOf(code) != -1;
    }
}
