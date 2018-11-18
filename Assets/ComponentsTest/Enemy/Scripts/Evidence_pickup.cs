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
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canPickUp = true;
        }

        if (Input.GetKeyDown("e") && canPickUp && !isPickedup)
        {
            evidenceMan.SendMessage("appendEvidence",evidenceInfo.EvidenceCode);
            isPickedup = true;
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
