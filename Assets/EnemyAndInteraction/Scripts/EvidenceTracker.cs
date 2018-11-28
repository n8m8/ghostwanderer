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
        evi1.SetActive(false);
        evi2.SetActive(false);
        evi3.SetActive(false);
        evi4.SetActive(false);
        evi5.SetActive(false);
        evi6.SetActive(false);
        evi7.SetActive(false);
        evi8.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(manager != null){
            updateEvidences();
        }
        else{
            if (GameObject.FindGameObjectWithTag("Evidences") == null)
            {

            }
            else
            {
                manager = GameObject.FindGameObjectWithTag("Evidences").gameObject.GetComponent<EvidenceManager>();
            }
        }
		
	}

    void updateEvidences(){
        List<int> evidences = manager.evidences;
        if(isInList(0,evidences)){
            evi1.SetActive(true);
        }
        else{
            evi1.SetActive(false);
        }

        if(isInList(1,evidences)){
            evi2.SetActive(true);
        }
        else{
            evi2.SetActive(false);
        }

        if (isInList(2, evidences))
        {
            evi3.SetActive(true);
        }
        else{
            evi3.SetActive(false);
        }
        if (isInList(3, evidences))
        {
            evi4.SetActive(true);
        }
        else{
            evi4.SetActive(false);
        }
        if (isInList(4, evidences))
        {
            evi5.SetActive(true);
        }
        else{
            evi5.SetActive(false);
        }

        if (isInList(5, evidences))
        {
            evi6.SetActive(true);
        }
        else{
            evi6.SetActive(false);
        }
        if (isInList(6, evidences))
        {
            evi7.SetActive(true);
        }
        else{
            evi7.SetActive(false);
        }
        if (isInList(7, evidences))
        {
            evi8.SetActive(true);
        }
        else{
            evi8.SetActive(false);
        }
    }

    bool isInList(int code, List<int> list){
        return list.IndexOf(code) != -1;
    }
}
