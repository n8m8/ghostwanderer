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

    private GameObject player;
    private AIDestinationSetter agent;
    private Transform target;
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

        if (state != AIState.chasing && triggerObject.GetComponent<InteractingObject>().isOn)
        {
            StartCoroutine(checking());
            return;
        }
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
        target = points[0];
        if(alarm.isOn){
            state = AIState.chasing;
            agent.target = playerPosition;
        }
    }

    IEnumerator checking(){
        setting.maxSpeed = 3.0f;
        target = points[1];
        yield return new WaitForSeconds(15.0f);
        alarm.isOn = false;
        triggerObject.GetComponent<InteractingObject>().isOn = false;
        state = AIState.sitting;
        target = points[0];
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
            state = AIState.sitting;
            alarm.isOn = false;
            agent.target = points[0];
        }
    }





}
