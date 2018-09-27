﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
    [SerializeField] private float abs_force = 50.0f;
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float friction = -10.0f;

    private Vector2 movement;
    private Rigidbody2D playerRB;

    private readonly float TAN27 = 1.96261050551f;

    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * ySpeed;

        //transform.Translate(x, y, 0.0f);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);

        playerRB.AddForce(playerRB.velocity * friction);
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

    // IEnumerator OnCollisionEnter2D(Collision2D collision)
    // {
    //     var hit = collision.gameObject; 
    //     if (hit.name.Contains("Chair"))
    //     {
    //         Destroy(gameObject);
    //         yield return new WaitForSeconds(2);
            
    //     }
    //     else if (hit.name.Contains("table"))
    //     {
    //         //Destroy(gameObject);
    //         yield return new WaitForSeconds(1);
    //         gameObject.GetComponent<Renderer>().enabled = true;
    //     }
    // }
}