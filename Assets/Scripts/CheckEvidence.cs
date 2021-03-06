﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckEvidence : MonoBehaviour {

    public InputManager IM_script;
    public GameObject EvidencePanel;
    public Text EvidencePanelText;

    [SerializeField] private GameObject ChildImage;
    [SerializeField] private float GeneralHeight = 350.0f;

    private Transform evidence_TRANS;
    private bool evidence_open_flag;

    private void Awake()
    {
        IM_script.OnInteractButtonPressed += CIIE_OTBP;
    }

    // Use this for initialization
    void Start () {
        this.evidence_TRANS = null;
        this.evidence_open_flag = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EvidenceTrigger")
        {
            evidence_TRANS = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EvidenceTrigger")
        {
            evidence_TRANS = null;
        }
    }

    private void CIIE_OTBP()    //CheckIfInEvidence to OnInteractButtonPressed;
    {
        if(!evidence_open_flag)
        {
            if (evidence_TRANS != null)
            {
                open_evidence();
                evidence_open_flag = true;
            }
        }
        else
        {
            close_evidence();
            evidence_open_flag = false;
        }


    }

    private void open_evidence()
    {
        ChildImage.GetComponent<Image>().sprite =
                                evidence_TRANS.GetComponent<Evidence>().EvidenceSprite;
        ChildImage.GetComponent<RectTransform>().sizeDelta = evidence_ratio_cal();
        EvidencePanelText.text = evidence_TRANS.GetComponent<Evidence>().evidence_text;
        EvidencePanel.SetActive(true);
        
    }

    private void close_evidence()
    {
        EvidencePanel.SetActive(false);
    }

    private Vector2 evidence_ratio_cal()
    {
        Rect evi_rect = evidence_TRANS.GetComponent<Evidence>().EvidenceSprite.rect;

        float ratio = evi_rect.height / GeneralHeight;

        return new Vector2(evi_rect.width / ratio, GeneralHeight);

    }

}
