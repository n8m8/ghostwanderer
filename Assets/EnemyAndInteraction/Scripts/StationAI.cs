using System.Collections;
using Pathfinding;
using UnityEngine;
public class StationAI : MonoBehaviour
{

    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    public GameObject enemyTrigger;

    public bool isTargetingGhost;
    public bool distracted = false;
    public bool isStuned = false;

    private GameObject player;
    private AIDestinationSetter agent;
    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
    private Vector3 startPosPlayer;
    private AIState state;
    private RaycastHit2D[] raycastHits = new RaycastHit2D[1];
    private TestPlayerMove playerControl;
    private AIPath setting;
    private Alarm alarm;

    private float distance;

    enum AIState
    {
        chasing,
        checking,
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
        agent = GetComponent<AIDestinationSetter>();
        //isChecking = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        now = transform.position;
        if (now != last)
        {
            currentDirection = (now - last) / Time.deltaTime;
        }
        last = transform.position;
        now.z = 0;
        transform.position = now;

        if (isStuned == false)
        {
            switch (state)
            {
                case AIState.sitting:
                    sitting();
                    break;
                case AIState.chasing:
                    chasing();
                    break;
                case AIState.checking:
                    checking();
                    break;
            }
        }
        else{
            setting.maxSpeed = 0f;
            state = AIState.sitting;
            agent.target = null;
        }

        killingGhost();
    }

    void sitting(){
        setting.maxSpeed = 2.0f;
        agent.target = points[0];
        if(alarm.isOn && playerControl.isGhost == isTargetingGhost){
            state = AIState.chasing;
            agent.target = playerPosition;
        }
        else{
            if(distracted){
                state = AIState.checking;
                agent.target = points[1];
            }
        }
    }

    void checking(){
        setting.maxSpeed = 3.0f;
        agent.target = points[1];
        /*
        setting.maxSpeed = 3.0f;
        agent.target = points[1];
        yield return new WaitForSeconds(10.0f);
        state = AIState.sitting;
        agent.target = points[0];
        */
    }

    void chasing()
    {
        setting.maxSpeed = 6.0f;
        setting.constrainInsideGraph = true;
        if ((playerControl.isGhost && isTargetingGhost == false) || (playerControl.isGhost == false && isTargetingGhost == true) || alarm.isOn == false)
        {
            state = AIState.sitting;
            agent.target = points[0]; ;
        }
        else{
            agent.target = playerPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Do something you want if you need to use checkpointetc
        if (collision.gameObject.CompareTag("Player") && playerControl.isGhost == isTargetingGhost)
        {
            if (playerControl.isGhost == false)
            {
                player.transform.position = player.GetComponent<CheckPointManager>().checkPoint.transform.position;
                this.transform.position = points[0].position;
                state = AIState.sitting;
                alarm.isOn = false;
                agent.target = points[0];
            }
        }
    }

    public float getDistance()
    {
        return distance;
    }


    void killingGhost()
    {
        if (distance < 0.5f && isTargetingGhost && playerControl.isGhost == true)
        {
            player.gameObject.GetComponent<TestPlayerMove>().turnToHuman();
            player.transform.position = player.GetComponent<CheckPointManager>().checkPoint.transform.position;
            this.transform.position = points[0].position;
            state = AIState.sitting;
            alarm.isOn = false;
            agent.target = points[0];
        }
    }




}
