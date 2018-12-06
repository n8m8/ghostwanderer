using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EvidenceManager : MonoBehaviour {
    public List<int> evidences;
    private TextMeshProUGUI textDisplay;
    private Scene scene;
    private bool switched;

	// Use this for initialization
    void Start () {
        switched = false;
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
        updateEvidence();
    }

    public void updateEvidence(){
        int count = 0;
        scene = SceneManager.GetActiveScene();
        if (scene.name == "FirstLevel"){
            int numEvidence = 5;
            if (evidences.Contains(0)){
                count++;
            }
            if (evidences.Contains(1)){
                count++;
            }
            if (evidences.Contains(4)){
                count++;
            }
            if (evidences.Contains(5)){
                count++;
            }
            if (evidences.Contains(6)){
                count++;
            }
            textDisplay.text = "Evidence Collected: " + evidences.Count.ToString() + "/" + numEvidence.ToString();
        }
        else if (scene.name == "SecondLevel"){
            if (switched == false)
            {
                textDisplay = GameObject.FindGameObjectWithTag("EvidenceDisplay").GetComponent<TextMeshProUGUI>();
                switched = true;
            }
            int numEvidence = 2;
            if (evidences.Contains(2)){
                count++;
            }
            if (evidences.Contains(3)){
                count++;
            }
            textDisplay.text = "Evidence Collected: " + count.ToString() + "/" + numEvidence.ToString();
        }

        
    }
}
