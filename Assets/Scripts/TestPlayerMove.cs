﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class TestPlayerMove : MonoBehaviour
{
    [SerializeField] private float abs_force = 50.0f;
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float friction = -10.0f;
    [SerializeField] private LevelController levelController;

    public Vector3 centerPt;
    //adjust for alter
    public float radius = 5f;

    private Vector2 movement;
    private Rigidbody2D playerRB;
    private PlayerController.PlayerStatus playerStatus;
    private Vector3 ghostPosition;
    private Vector3 spawnPosition;
    private bool canMoveObject = false;
    private bool enemyInRange = false;


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
            Vector3 newPos = transform.position; 
        
        if (isGhost){
            Vector3 offset = newPos - centerPt;
            transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
        }
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
        if(other.name == "Vase Trigger")
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
    public bool isGhost = false;
    public void toggleGhostMode()
    {
        BoxCollider2D PC = gameObject.GetComponent<BoxCollider2D>();
        PC.isTrigger = true;
        if (isGhost)
        {
            Destroy(GameObject.Find("TestPlayer(Clone)"));
            this.gameObject.GetComponent<SpriteRenderer>().sprite = humanSprite;
            isGhost = false;
            PC.isTrigger = false;
            //ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            //system.Stop();
            gameObject.transform.position = ghostPosition;

        }
        else
        {
            GameObject copy = (GameObject) Instantiate(gameObject);
            copy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            centerPt = transform.position;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
            ghostPosition = gameObject.transform.position;
            PC.isTrigger = true;
            isGhost = true;
            //ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            //system.Play();
        }
    }

}
