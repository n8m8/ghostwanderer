using System.Collections;
using Pathfinding;
using UnityEngine;
public class StationAI : MonoBehaviour
{

    public Transform[] points;
    public float fieldOfViewAngle = 30f;
    public Transform playerPosition;
    public GameObject enemyTrigger;
    public GameObject triggerObject;

    public bool isTargetingGhost;
    public bool distracted = false;

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
        startPosPlayer = player.transform.position;
        agent = GetComponent<AIDestinationSetter>();
        //isChecking = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        now = transform.position;
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
            case AIState.checking:
                checking();
                break;
            }

    }

    void sitting(){
        setting.maxSpeed = 2.0f;
        agent.target = points[0];
        if(alarm.isOn){
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
        triggerObject.GetComponent<InteractingObject>().isOn = false;*/
    }

    void chasing(float distance)
    {
        setting.maxSpeed = 6.0f;
        setting.constrainInsideGraph = true;
        if (playerControl.isGhost || alarm.isOn == false)
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
            player.transform.position = startPosPlayer;
            state = AIState.sitting;
            alarm.isOn = false;
            agent.target = points[0];
        }
    }





}
