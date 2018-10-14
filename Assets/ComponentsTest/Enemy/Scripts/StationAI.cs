using System.Collections;
using Pathfinding;
using UnityEngine;
public class StationAI : MonoBehaviour
{

    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    public GameObject enemyTrigger;

    private GameObject player;
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
    private AIPath setting;
    private Alarm alarm;

    enum AIState
    {
        chasing,
        confusing,
        sitting
    }

    // Use this for initialization
    void Start()
    {
        state = AIState.sitting;
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        setting = this.GetComponent<AIPath>();
        alarm = enemyTrigger.GetComponent<Alarm>();
        startPosPlayer = player.transform.position;
        agent = GetComponent<AIDestinationSetter>();
        target = agent.target;
        temp = Instantiate(new GameObject()).transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        now = transform.position;
        Debug.Log(alarm.isOn);
        if (now != last)
        {
            currentDirection = (now - last) / Time.deltaTime;
        }
        last = transform.position;
        now.z = 0;
        transform.position = now;

        switch (state)
        {
            case AIState.sitting:
                sitting();
                break;
            case AIState.chasing:
                chasing(distance);
                break;
        }
    }

    void sitting(){
        setting.maxSpeed = 2.0f;
        if(alarm.isOn){
            state = AIState.chasing;
            target = playerPosition;
        }
    }

    void chasing(float distance)
    {
        setting.maxSpeed = 15.0f;
        setting.constrainInsideGraph = true;
        if (alarm.isOn == false)
        {
            state = AIState.sitting;
            agent.target = points[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Do something you want if you need to use checkpointetc
        if (collision.gameObject.CompareTag("Player") && playerControl.isGhost == false)
        {
            player.transform.position = startPosPlayer;
            state = AIState.confusing;
            temp.position = transform.position;
            alarm.isOn = false;
            agent.target = null;
        }
    }





}
