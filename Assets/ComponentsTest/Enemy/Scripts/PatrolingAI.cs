﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingAI : MonoBehaviour {
    public Transform[] points;
    private int desPoint = 0;
    private AIDestinationSetter agent;
    private Transform target;
	// Use this for initialization
	void Start () {
        agent = GetComponent<AIDestinationSetter>();
        target = agent.target;
        GotoNextPoint();
	}

    void GotoNextPoint(){
        if (points.Length == 0)
            return;
        desPoint = (desPoint + 1) % points.Length;
        target = points[desPoint];
        agent.target = target;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(this.gameObject.transform.position, target.transform.position) < 0.5f)
        {
            print(target.transform.position);
            GotoNextPoint();
        }
	}
}
