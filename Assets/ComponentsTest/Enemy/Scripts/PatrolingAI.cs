using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingAI : MonoBehaviour {
    
    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    private GameObject player;
    private bool isChasing;
    private bool isPatrolling;
    private bool isSearching;
    private bool seePlayer;
    private int desPoint = 0;
    private AIDestinationSetter agent;
    private Transform target;
    private Transform temp;
    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
   




    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        isChasing = false;
        seePlayer = false;
        isPatrolling = true;
        isSearching = false;
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
        checkLOS();
        now = transform.position;
        if (now != last)
        {
            currentDirection = (now - last) / Time.deltaTime;
        }
        last = transform.position;

        if (isPatrolling && !isChasing && !isSearching)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.5f)
            {
                GotoNextPoint();
            }
            if (Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle && seePlayer)
            {
              
                isChasing = true;
                isSearching = false;
                isPatrolling = false;
                agent.target = playerPosition;
                return;
            }
        }
        else if (isChasing && !isPatrolling && !isSearching)
        {
           
            if (Vector3.Angle(player.transform.position - transform.position, currentDirection) >= fieldOfViewAngle && !seePlayer)
            {
                isChasing = false;
                isSearching = true;
                isPatrolling = false;
                temp.transform.position = player.transform.position;
                agent.target = temp;
                return;
            }
            else{
               
                isChasing = true;
                isSearching = false;
                isPatrolling = false;
                agent.target = playerPosition;
                return;
            }
        }
        else if(isSearching && !isChasing && !isPatrolling){
           
            if (Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle && seePlayer)
            {
                isChasing = true;
                isSearching = false;
                isPatrolling = false;
                agent.target = playerPosition;
                return;
            }
            else
            {
                isChasing = false;
                isSearching = false;
                isPatrolling = true;
                GotoNextPoint();
                return;
            }
        }
        else{
            isChasing = false;
            isSearching = false;
            isPatrolling = true;
            GotoNextPoint();
            return;
        }
    }

    IEnumerator wait()
    {       
        yield return new WaitForSeconds(1.5f);
    }

    void checkLOS(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position,player.transform.position - transform.position,500f,1<<LayerMask.NameToLayer("TransparentFX"));
        if (hit)
        {
            if (hit.transform == player.transform)
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }
        }
        else{
            Debug.Log("Nothing hit");
        }
    }







}
