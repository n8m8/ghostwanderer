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
    private Animator animator;

    private float distance;
    private string prevAnimState = "";

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
        animator = GetComponent<Animator>();
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
        //Debug.LogWarning(currentDirection.magnitude);
        ToggleAnimations();
        if (isStuned == false)
        {
            animator.SetBool("shocked", false);
            switch (state)
            {
                case AIState.sitting:
                    sitting();
                    break;
                case AIState.chasing:
                    chasing();
                    animator.speed = Mathf.Log(getDirection().magnitude) * .8f;
                    break;
                case AIState.checking:
                    checking();
                    animator.speed = Mathf.Log(getDirection().magnitude) * 35f;
                    break;
            }
        }
        else{
            animator.SetBool("shocked", true);
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


    public Vector3 getDirection()
    {
        return currentDirection;
    }

    private void ToggleAnimations()
    {
        float h = getDirection().x;
        float v = getDirection().y;
        if (h > 0)
        {
            if (v > 0)
            {
                SetAnimRightUp();
            }
            else if (v < 0)
            {
                SetAnimRightDown();
            }
            else
            {
                if (prevAnimState.Equals("leftUp") || prevAnimState.Equals("rightUp"))
                {
                    SetAnimRightUp();
                }
                else
                {
                    SetAnimRightDown();
                }
            }
            animator.SetBool("idle", false);
        }
        else if (h < 0)
        {
            if (v > 0)
            {
                SetAnimLeftUp();
            }
            else if (v < 0)
            {
                SetAnimLeftDown();
            }
            else
            {
                if (prevAnimState.Equals("leftUp") || prevAnimState.Equals("rightUp"))
                {
                    SetAnimLeftUp();
                }
                else
                {
                    SetAnimLeftDown();
                }
            }
            animator.SetBool("idle", false);
        }
        else    //playerRB.velocity.x == 0
        {
            if (v > 0)
            {
                if (prevAnimState.Equals("leftUp") || prevAnimState.Equals("leftDown"))
                {
                    SetAnimLeftUp();
                }
                else
                {
                    SetAnimRightUp();
                }
                animator.SetBool("idle", false);
            }
            else if (v < 0)
            {
                if (prevAnimState.Equals("leftUp") || prevAnimState.Equals("leftDown"))
                {
                    SetAnimLeftDown();
                }
                else
                {
                    SetAnimRightDown();
                }
                animator.SetBool("idle", false);
            }
            else
            {
                switch (prevAnimState)
                {
                    case "leftUp":
                        SetAnimLeftUp();
                        break;
                    case "leftDown":
                        SetAnimLeftDown();
                        break;
                    case "rightUp":
                        SetAnimRightUp();
                        break;
                    case "rightDown":
                        SetAnimRightDown();
                        break;
                    default:
                        break;
                }
                animator.SetBool("idle", true);
            }
        }
    }

    private void SetAnimLeftUp()
    {
        animator.SetBool("leftDown", false);
        animator.SetBool("leftUp", true);
        animator.SetBool("rightDown", false);
        animator.SetBool("rightUp", false);
        prevAnimState = "leftUp";
    }

    private void SetAnimLeftDown()
    {
        animator.SetBool("leftDown", true);
        animator.SetBool("leftUp", false);
        animator.SetBool("rightDown", false);
        animator.SetBool("rightUp", false);
        prevAnimState = "leftDown";
    }

    private void SetAnimRightUp()
    {
        animator.SetBool("leftDown", false);
        animator.SetBool("leftUp", false);
        animator.SetBool("rightDown", false);
        animator.SetBool("rightUp", true);
        prevAnimState = "rightUp";
    }

    private void SetAnimRightDown()
    {
        animator.SetBool("leftDown", false);
        animator.SetBool("leftUp", false);
        animator.SetBool("rightDown", true);
        animator.SetBool("rightUp", false);
        prevAnimState = "rightDown";
    }


}
