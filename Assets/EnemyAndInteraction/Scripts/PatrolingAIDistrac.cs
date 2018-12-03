using System.Collections;
using Pathfinding;
using UnityEngine;
public class PatrolingAIDistrac : MonoBehaviour
{

    public Transform[] points;
    public Transform distraction;
    public Transform playerPosition;
    private Transform target;
    private Transform temp;

    public GameObject enemyTrigger;
    private GameObject player;

    private AIDestinationSetter agent;

    public bool isTargetingGhost = false;
    public bool distracted = false;
    private bool seePlayer;


    private int desPoint = 0;
    public float fieldOfViewAngle = 30f;
    private float t = 0;

    private Vector3 last;
    private Vector3 now;
    private Vector3 currentDirection;
    private AIState state;

    private RaycastHit2D[] raycastHits = new RaycastHit2D[1];
    private TestPlayerMove playerControl;
    private AIPath setting;
    private Alarm alarm;
    private Animator animator;
    private string prevAnimState = "";
    

    public bool isStuned;

    enum AIState
    {
        chasing,
        confusing,
        patrolling,
        distracted
    }

    // Use this for initialization
    void Start()
    {
        Vector3 now = transform.position;
        now.z = 0;
        t = 0;
        transform.position = now;
        state = AIState.patrolling;
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        setting = this.GetComponent<AIPath>();
        alarm = enemyTrigger.GetComponent<Alarm>();
        seePlayer = false;
        agent = GetComponent<AIDestinationSetter>();
        target = agent.target;
        temp = Instantiate(new GameObject()).transform;
	animator = GetComponent<Animator>();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        desPoint = (desPoint + 1) % points.Length;
        target = points[desPoint];
        agent.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if(t == 0f){
            last = transform.position;
        }
        t = t + Time.deltaTime;
        if (t >= 0.5f)
        {
            now = transform.position;
            currentDirection = (now - last) /t;
            t = 0f;
        }
        now.z = 0;
        checkLOS();
	if (animator != null)
            ToggleAnimations();
        if(distracted){
            state = AIState.distracted;
            agent.target = distraction;
        }


        if (isStuned == false)
        {
            switch (state)
            {
                case AIState.patrolling:
		    if (animator != null)
                        animator.speed = getDirection().magnitude * .8f;
                    patrolling(distance);
                    break;
                case AIState.chasing:
                    chasing(distance);
		    if (animator != null)
                        animator.speed = getDirection().magnitude * 1f;
                    break;
                case AIState.confusing:
		    if (animator != null)
                        animator.speed = 1f;
                    StartCoroutine(confusing());
                    break;
                case AIState.distracted:
	 	    if (animator != null)
                        animator.speed = 1f;
                    break;
            }
        }
        else
        {
            setting.maxSpeed = 0.0f;
            state = AIState.confusing;
            temp.position = transform.position;
            agent.target = null;
        }
    }


    void checkLOS()
    {
        int hit = Physics2D.LinecastNonAlloc(transform.position, player.transform.position, raycastHits, 1 << LayerMask.NameToLayer("TransparentFX"));
        if (hit == 0 && Vector3.Angle(player.transform.position - transform.position, currentDirection) < fieldOfViewAngle)
        {
            seePlayer = true;
        }
        else
        {
            seePlayer = false;
        }
        //Debug.Log(seePlayer + " " + hit + " " + Vector3.Angle(player.transform.position - transform.position, currentDirection));
    }

    void patrolling(float distance)
    {
        setting.constrainInsideGraph = false;
        setting.maxSpeed = 1.0f;
        if (Vector2.Distance(transform.position, target.position) < 0.5f)
        {
            GotoNextPoint();
            return;
        }

        if ((seePlayer || alarm.isOn) && playerControl.isGhost == isTargetingGhost)
        {
            state = AIState.chasing;
            agent.target = playerPosition;
        }
    }

    void chasing(float distance)
    {
        setting.maxSpeed = 6.0f;
        setting.constrainInsideGraph = true;
        if ((!seePlayer && distance > 5.0f && alarm.isOn == false) || (playerControl.isGhost == true && isTargetingGhost == false))
        {
            state = AIState.confusing;
            temp.position = transform.position;
            agent.target = null;
        }
    }

    void distracting()
    {
        //agent.target = points[points.Length - 1];
    }

    IEnumerator confusing()
    {
        transform.position = now;
        setting.maxSpeed = 3.0f;
        setting.constrainInsideGraph = false;
        float distance = Vector2.Distance(player.transform.position, transform.position);
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
            player.transform.position = player.GetComponent<CheckPointManager>().checkPoint.transform.position;
            state = AIState.confusing;
            temp.position = transform.position;
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
