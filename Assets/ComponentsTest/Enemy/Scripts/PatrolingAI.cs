using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingAI : MonoBehaviour {
    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    private bool isChasing;
    private bool isPatrolling;
    private bool isFacing;
    private int desPoint = 0;
    private AIDestinationSetter agent;
    private Transform target;
    private Transform temp;
    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
   




    // Use this for initialization
    void Start () {
        isChasing = false;
        isFacing = false;
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
        now = transform.position;
        if (now != last){
            currentDirection = (now - last)/Time.deltaTime;
        }
        last = transform.position;

        print(Vector3.Angle(playerPosition.position - transform.position, currentDirection));

        if (!isChasing)
        {
            if (Vector2.Distance(this.gameObject.transform.position, target.transform.position) < 0.5f)
            {
                GotoNextPoint();
            }
            if (Vector3.Angle(playerPosition.position - transform.position,currentDirection) < fieldOfViewAngle)
            {
                isChasing = true;
                agent.target = playerPosition;
            }
        }
        else
        {
            if (Vector3.Angle(playerPosition.position - transform.position, currentDirection) >= fieldOfViewAngle)
            {
                isChasing = false;
                temp.transform.position = playerPosition.position;
                agent.target = temp;
                GotoNextPoint();
            }
        }
    }

    IEnumerator wait()
    {       
        yield return new WaitForSeconds(1.5f);
    }







}
