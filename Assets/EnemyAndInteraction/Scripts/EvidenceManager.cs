using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EvidenceManager : MonoBehaviour {
    public List<int> evidences;
    private TextMeshProUGUI textDisplay;
    private Scene scene;
    private int firstlevelcount = 0;

	// Use this for initialization
    void Start () {
        textDisplay = GameObject.FindGameObjectWithTag("EvidenceDisplay").GetComponent<TextMeshProUGUI>();
        scene = SceneManager.GetActiveScene();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void appendEvidence(int evidenceCode){
        if (evidences.IndexOf(evidenceCode) == -1)
        {
            evidences.Add(evidenceCode);
        }
        if (scene.name == "FirstLevel"){
            updateEvidence(5);
        }
        else if (scene.name == "SecondLevel"){
            updateEvidence(3);
        }
    }

    public void updateEvidence(int numEvidence){
        Debug.Log("lmaozedeong");
        if (scene.name == "FirstLevel"){
            textDisplay.text = "Evidence Collected: " + evidences.Count.ToString() + "/" + numEvidence.ToString();
            firstlevelcount++;
        }
        else if (scene.name == "SecondLevel"){
            int count = evidences.Count - firstlevelcount;
            textDisplay.text = "Evidence Collected: " + count.ToString() + "/" + numEvidence.ToString();
        }
        
    }
}
