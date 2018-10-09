using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class TestPlayerMove : MonoBehaviour
{
    [SerializeField] private float abs_force = 50.0f;
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float friction = -10.0f;

    private Vector2 movement;
    private Rigidbody2D playerRB;
    private PlayerController.PlayerStatus playerStatus;
    private Vector3 ghostPosition;
    private Vector3 spawnPosition;

    public Sprite ghostSprite;
    public Sprite humanSprite;

    private readonly float TAN27 = 1.96261050551f;
    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        //playerStatus = this.GetComponent<PlayerController>().playerStatus;
        ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
        system.Stop();
        spawnPosition = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * ySpeed;

        //transform.Translate(x, y, 0.0f);
        if (Input.GetKeyDown("q") && (canGhost || isGhost))
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
        //}
        //else
        //{
         //   Debug.Log("move allowed not working");
        //}
    }

    bool canGhost = false;
    void OnTriggerStay2D(Collider2D other)
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        if (other.tag == "Ghost Portal")
        {
            canGhost = true;
        }
        if (other.tag == "Map Trigger")
        {
            PC.isTrigger = false;
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
    public bool isGhost = false;
    public void toggleGhostMode()
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        PC.isTrigger = true;
        if (isGhost)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
            isGhost = false;
            PC.isTrigger = false;
            //ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            //system.Stop();
            gameObject.transform.position = ghostPosition;

        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
            ghostPosition = gameObject.transform.position;
            PC.isTrigger = true;
            isGhost = true;
            //ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            //system.Play();
        }
    }

}
