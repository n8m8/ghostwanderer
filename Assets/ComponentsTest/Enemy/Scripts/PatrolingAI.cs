﻿using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingAI : MonoBehaviour {
    
    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    private GameObject player;
    private bool seePlayer;
    private int desPoint = 0;
    private AIDestinationSetter agent;
    private Transform target;
    private Transform temp;
    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
    private Vector3 startPosPlayer;
    private AIState state;
    private RaycastHit2D[] raycastHits = new RaycastHit2D[1];
    private TestPlayerMove playerControl;

    enum AIState{
        chasing,
        confusing,
        patrolling
    }

    // Use this for initialization
    void Start () {
        state = AIState.patrolling;
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        startPosPlayer = player.transform.position;
        seePlayer = false;
        agent = GetComponent<AIDestinationSetter>();
        target = agent.target;
        temp = Instantiate(new GameObject()).transform;
	}

    void GotoNextPoint(){
        if (points.Length == 0)
            return;
        desPoint = (desPoint + 1) % points.Length;
        target = points[desPoint];
        agent.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        now = transform.position;
        if (now != last)
        {
            currentDirection = (now - last) / Time.deltaTime;
        }
        last = transform.position;

        switch(state){
            case AIState.patrolling:
                patrolling();
                break;
            case AIState.chasing:
                chasing();
                break;
            case AIState.confusing:
                StartCoroutine(confusing());
                break;
        }
    }


    void checkLOS(){
        int hit = Physics2D.LinecastNonAlloc(transform.position, player.transform.position, raycastHits, 1 << LayerMask.NameToLayer("TransparentFX"));

            if (hit == 0 && Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle)
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }

    }

    void patrolling(){
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            GotoNextPoint();
            return;
        }
        checkLOS();
        if (seePlayer && playerControl.isGhost == !enabled)
        {
            state = AIState.chasing;
            agent.target = playerPosition;
        }
    }

    void chasing(){
        float distance = Vector2.Distance(player.transform.position, transform.position);
        checkLOS();
        if(!seePlayer && distance > 5.0f)
        {
            state = AIState.confusing;
            temp.position = transform.position;
            agent.target = null;
        }
    }

    IEnumerator confusing(){
        float distance = Vector2.Distance(player.transform.position, transform.position);
        temp.position = transform.position;
        agent.target = temp;
        yield return new WaitForSeconds(2.0f);
        state = AIState.patrolling;
        GotoNextPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Do something you want if you need to use checkpointetc
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = startPosPlayer;
        }
    }






}
