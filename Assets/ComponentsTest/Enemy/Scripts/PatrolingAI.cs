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
        checkLOS();
        now = transform.position;
        if (now != last){
            currentDirection = (now - last)/Time.deltaTime;
        }
        last = transform.position;

        if (isPatrolling)
        {
            Debug.Log("patrolling");
            if (Vector2.Distance(this.gameObject.transform.position, target.transform.position) < 0.5f)
            {
                Debug.Log("is patrolling");
                isChasing = false;
                isSearching = false;
                isPatrolling = true;
                GotoNextPoint();
            }
            if (Vector3.Angle(player.transform.position - transform.position,currentDirection) < fieldOfViewAngle && seePlayer)
            {
                Debug.Log("start chasing");
                isChasing = true;
                isSearching = false;
                isPatrolling = false;
                agent.target = playerPosition;
            }
        }
        else if(isChasing)
        {
            Debug.Log("is Chasing");
            if (Vector3.Angle(player.transform.position - transform.position, currentDirection) >= fieldOfViewAngle && !seePlayer)
            {
                Debug.Log("lose player");
                isChasing = false;
                isSearching = true;
                isPatrolling = false;
                temp.transform.position = player.transform.position;
                agent.target = temp;
            }
            else{
                agent.target = playerPosition;
            }
        }
        else if(isSearching){
            Debug.Log("search player");
            if (Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle && seePlayer)
            {
                Debug.Log("keep chasing");
                isChasing = true;
                isSearching = false;
                isPatrolling = false;
                agent.target = playerPosition;
            }

            if (Vector2.Distance(this.gameObject.transform.position, temp.transform.position) < 0.5f && !seePlayer)
            {
                Debug.Log("stop chasing");
                isChasing = false;
                isSearching = false;
                isPatrolling = true;
                GotoNextPoint();
            }
            else{
                agent.target = temp;
            }
        }
        else{

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
