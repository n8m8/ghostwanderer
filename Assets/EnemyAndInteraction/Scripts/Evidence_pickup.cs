using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence_pickup : MonoBehaviour {
    private bool canPickUp;
    private bool isPickedup;
    private Evidence evidenceInfo;
    private GameObject evidenceMan;

    // Use this for initialization
    void Start()
    {
        evidenceInfo = this.GetComponent<Evidence>();
        evidenceMan = GameObject.FindWithTag("Evidences");
        isPickedup = false;
        canPickUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(evidenceMan == null){
            evidenceMan = GameObject.FindWithTag("Evidences");
        }

        if (Input.GetKeyDown("e") && canPickUp && !isPickedup)
        {
            evidenceMan.SendMessage("appendEvidence", evidenceInfo.EvidenceCode);
            isPickedup = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPickUp = false;
        }

    }
}
