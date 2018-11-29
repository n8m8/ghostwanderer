using System.Collections;
using Pathfinding;
using UnityEngine;
public class PatrolingAI : MonoBehaviour {
    
    public Transform[] points;
    public Transform playerPosition;
    private Transform target;
    private Transform temp;

    public GameObject enemyTrigger;
    private GameObject player;

    private AIDestinationSetter agent;

    public bool isTargetingGhost = false;

    private bool seePlayer;


    private int desPoint = 0;
    public float fieldOfViewAngle = 30f;
    private float distance;

    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
    private Vector3 startPosPlayer;
    private AIState state;

    private RaycastHit2D[] raycastHits = new RaycastHit2D[1];
    private TestPlayerMove playerControl;
    private AIPath setting;

    public bool isStuned;

    enum AIState{
        chasing,
        confusing,
        patrolling,
    }

    // Use this for initialization
    void Start () {
        Vector3 now = transform.position;
        now.z = 0;
        transform.position = now;
        state = AIState.patrolling;
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        setting = this.GetComponent<AIPath>();
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
        distance = Vector2.Distance(player.transform.position, transform.position);
        now = transform.position;
        if (now != last)
        {
            currentDirection = (now - last) / Time.deltaTime;
        }
        last = transform.position;
        now.z = 0;
        transform.position = now;
        checkLOS();



        if (isStuned == false)
        {
            switch (state)
            {
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
        else{
            setting.maxSpeed = 0.0f;
            state = AIState.confusing;
            temp.position = transform.position;
            agent.target = null;
        }

        killingGhost();
    }

    public Vector3 getDirection(){
        return currentDirection;
    }


    void checkLOS(){
        int hit = Physics2D.LinecastNonAlloc(transform.position, player.transform.position, raycastHits, 1 << LayerMask.NameToLayer("TransparentFX"));
        if (hit == 0 && Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle && playerControl.isGhost == isTargetingGhost)
            {
                seePlayer = true;
            }
            else
            {
                seePlayer = false;
            }
    }

    void patrolling(){
        setting.constrainInsideGraph = false;
        setting.maxSpeed = 1.0f;
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            GotoNextPoint();
            return;
        }

        if ((seePlayer || enemyTrigger.GetComponent<Alarm>().isOn)&& playerControl.isGhost == isTargetingGhost)
        {
            state = AIState.chasing;
            agent.target = playerPosition;
        }
    }

    void chasing(){
        setting.maxSpeed = 6.0f;
        setting.constrainInsideGraph = true;
        if ((!seePlayer && distance > 3.0f && enemyTrigger.GetComponent<Alarm>().isOn == false) || (playerControl.isGhost == true && isTargetingGhost == false) || (playerControl.isGhost == false && isTargetingGhost == true))
        {
            state = AIState.confusing;
            temp.position = transform.position;
            agent.target = null;
        }
    }

    void distracting(){
       //agent.target = points[points.Length - 1];
    }

    void killingGhost()
    {
        if (distance < 0.5f && isTargetingGhost && playerControl.isGhost == true)
        {
            player.gameObject.GetComponent<TestPlayerMove>().turnToHuman();
            player.transform.position = player.transform.position = player.GetComponent<CheckPointManager>().checkPoint.transform.position;
            temp.position = transform.position;
            enemyTrigger.GetComponent<Alarm>().isOn = false;
            this.transform.position = points[0].position;
            state = AIState.patrolling;
            GotoNextPoint();
        }
    }

    IEnumerator confusing(){
        enemyTrigger.GetComponent<Alarm>().isOn = false;
        transform.position = now;
        setting.maxSpeed = 3.0f;
        setting.constrainInsideGraph = false;
        distance = Vector2.Distance(player.transform.position, transform.position);
        agent.target = null;
        yield return new WaitForSeconds(2.0f);
        state = AIState.patrolling;
        GotoNextPoint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(isTargetingGhost + " " + playerControl.isGhost);
        //Do something you want if you need to use checkpointetc
        if (collision.gameObject.CompareTag("Player") && playerControl.isGhost == isTargetingGhost)
        {

            if (playerControl.isGhost == false)
            {
                player.transform.position = player.GetComponent<CheckPointManager>().checkPoint.transform.position;
                temp.position = transform.position;
                enemyTrigger.GetComponent<Alarm>().isOn = false;
                this.transform.position = points[0].position;
                state = AIState.patrolling;
                GotoNextPoint();
            }
        }
    }

    public float getDistance(){
        return distance;
    }

    





}
