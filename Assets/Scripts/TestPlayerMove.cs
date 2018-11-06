using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class TestPlayerMove : MonoBehaviour
{
    [SerializeField] public float abs_force = 50.0f;
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float friction = -10.0f;
    [SerializeField] private LevelController levelController;

    [SerializeField] private Vector3 centerPt;
    [SerializeField] private float radius = 5f;

	[SerializeField] private GameObject musicObject;

    [SerializeField] private Sprite ghostSprite;
    [SerializeField] private Sprite humanSprite;

    private Vector2 movement;
    private Rigidbody2D playerRB;
    private PlayerController.PlayerStatus playerStatus;
    private Vector3 ghostPosition;
    private Vector3 spawnPosition;
	private MusicController musicController;
    private Animator animator;
    private bool canMoveObject = false;
    private bool enemyInRange = false;

    public bool isGhost = false;
    public bool ghostAvailable = false;
    public bool bodyAvailable = false;

    private readonly float TAN27 = 1.96261050551f;
    private readonly float TOLERANCE = .9f;
    private float original;

    private GameObject hidingPlace;


    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
        system.Stop();
        spawnPosition = gameObject.transform.position;
		musicController = musicObject.GetComponent<MusicController> ();
        GameObject.Find("TestPlayer").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        original = abs_force;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && ghostAvailable && (canGhost || isGhost))
        {
            toggleGhostMode();
        }
    }


    private void FixedUpdate()
    {
        //if (playerStatus.moveAllowed)
        //{
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        playerRB.AddForce(playerRB.velocity * friction);
        Vector3 newPos = transform.position;

        if (isGhost) {
            Vector3 offset = newPos - centerPt;
            transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
        }

        ToggleAnimations();

    }

    private void ToggleAnimations()
    {
        if (playerRB.velocity.x > TOLERANCE)
        {
            if (playerRB.velocity.y > TOLERANCE)
            {
                animator.SetBool("rightUp", true);
                animator.SetBool("rightDown", false);
            }
            else
            {
                animator.SetBool("rightDown", true);
                animator.SetBool("rightUp", false);
            }
            animator.SetBool("leftDown", false);
            animator.SetBool("leftUp", false);
            animator.SetBool("idle", false);
        }
        else if (playerRB.velocity.x < -TOLERANCE)
        {
            if (playerRB.velocity.y > TOLERANCE)
            {
                animator.SetBool("leftUp", true);
                animator.SetBool("leftDown", false);
            }
            else
            {
                animator.SetBool("leftDown", true);
                animator.SetBool("leftUp", false);
            }
            animator.SetBool("rightDown", false);
            animator.SetBool("rightUp", false);
            animator.SetBool("idle", false);
        }
        else    //playerRB.velocity.x == 0
        {
            if (playerRB.velocity.y > TOLERANCE)
            {
                animator.SetBool("leftUp", true);
                animator.SetBool("leftDown", false);
                animator.SetBool("rightDown", false);
                animator.SetBool("rightUp", false);
                animator.SetBool("idle", false);
            }
            else if (playerRB.velocity.y < -TOLERANCE)
            {
                animator.SetBool("rightDown", true);
                animator.SetBool("leftUp", false);
                animator.SetBool("leftDown", false);
                animator.SetBool("rightUp", false);
                animator.SetBool("idle", false);
            }
            else
            {
                animator.SetBool("leftDown", false);
                animator.SetBool("leftUp", false);
                animator.SetBool("rightDown", false);
                animator.SetBool("rightUp", false);
                animator.SetBool("idle", true);
            }
        }
    }

    bool canGhost = false;


    void OnTriggerStay2D(Collider2D other)
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        if (other.tag == "Ghost Portal")
        {
            canGhost = true;
            hidingPlace = other.gameObject;
        }
        if (other.tag == "Map Trigger")
        {
            PC.isTrigger = false;
        }
        if (other.name == "Vase Trigger")
        {
            canMoveObject = true;
        }


        if (Input.GetKeyDown("f") && canMoveObject && enemyInRange)
        {
            levelController.Vase.GetComponent<Renderer>().enabled = false;
            //Destroy(levelController.Vase);
            //Destroy(levelController.VaseObject);   
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        canGhost = false;
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();

        if (collision.tag == "Map Trigger")
        {
            PC.isTrigger = true;
        }
    }


    private void Move(float h, float v)
    {
        //Modifies movement to match the angles of the tile map.
        h = h * TAN27;

        movement.Set(h, v);
        movement = movement.normalized * abs_force * Time.deltaTime;

        playerRB.AddForce(movement);

        //if (playerRB.velocity.magnitude > topSpeed)
        //    playerRB.velocity = playerRB.velocity.normalized * topSpeed;
    }


    public void toggleGhostMode()
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        PC.isTrigger = true;
        if (isGhost && bodyAvailable)
        {
            //Destroy(GameObject.Find("TestPlayer(Clone)"));
            this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
            isGhost = false;
            PC.isTrigger = false;
            ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            system.Play();
            gameObject.transform.position = ghostPosition;
            //hidingPlace.GetComponentInParent<BodyHideObject>().ContainsBody = false;
            animator.SetBool("isGhost", false);
        }
        else if (!isGhost)
        {
            //GameObject copy = (GameObject)Instantiate(gameObject);
           // copy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            centerPt = transform.position;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
            ghostPosition = gameObject.transform.position;
            PC.isTrigger = true;
            isGhost = true;
            animator.SetBool("isGhost", true);
            //hidingPlace.GetComponentInParent<BodyHideObject>().ContainsBody = true;

            ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            system.Play();
        }
        musicController.isGhost = isGhost;
    }

    public void EnableGhostMode()
    {
        ghostAvailable = true;
        toggleGhostMode();
    }

    public void FindBody()
    {
        bodyAvailable = true;
        ChangeRadius(5f);
        StartCoroutine(FindBodyAnimation()); 
    }

    private IEnumerator FindBodyAnimation()
    {
        Debug.Log("Body Found!!!");
        yield return new WaitForSeconds(1f);
        ReturnToHuman();
    }

    public bool GetBodyAvailable()
    {
        return bodyAvailable;
    }

    private void ReturnToHuman()
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
        isGhost = false;
        PC.isTrigger = false;
    }

    public void DisableMovement(){
        abs_force = 0f;
    }

    public void EnableMovement(){
        abs_force = original;
    }

    public void ChangeRadius(float newRadius)
    {
        radius = newRadius;
    }
}
