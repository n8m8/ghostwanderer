﻿using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{
    public bool isTargetingGhost = false;
    private GameObject player;
    private TestPlayerMove playerControl;
    public bool isOn;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerControl.isGhost == isTargetingGhost)
        {
            isOn = true;
        }
    }
}
