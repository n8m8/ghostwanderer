using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour {
    public List<int> evidences;
	// Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void appendEvidence(int evidenceCode){
        evidences.Add(evidenceCode);
    }
}
