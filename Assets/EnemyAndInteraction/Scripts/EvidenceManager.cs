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
        if (evidences.IndexOf(evidenceCode) == -1)
        {
            evidences.Add(evidenceCode);
        }
    }
}
