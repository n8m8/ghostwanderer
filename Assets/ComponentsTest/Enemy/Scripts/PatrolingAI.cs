using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingAI : MonoBehaviour {
    public Transform[] points;
    private int desPoint = 0;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
	}

    void GotoNextPoint(){
        if (points.Length == 0)
            return;

        agent.destination = points[desPoint].position;
        desPoint = (desPoint + 1) % points.Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (!agent.pathPending && agent.remainingDistance < 8.0f)
            GotoNextPoint();
	}
}
