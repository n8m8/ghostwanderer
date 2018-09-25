using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{

    //public float ySpeed = 10.0f;
    //public float xSpeed = 10.0f;

    public float force = 50.0f;
    public float topSpeed = 6.0f;
    public float friction = -10.0f;

    private Vector2 movement;
    private Rigidbody2D playerRB;

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
        if(h == v)
        {
            h = h * 2;
        }
        else if (h == v * -1)
        {
            h = h * 2;
        }
        movement.Set(h, v);
        movement = movement.normalized * force * Time.deltaTime;

        //Debug.Log("movement " + movement);

        //Debug.Log("movement " + movement);

        //playerRB.MovePosition(transform.position + movement);
        playerRB.AddForce(movement * force);


        if (playerRB.velocity.magnitude > topSpeed)
            playerRB.velocity = playerRB.velocity.normalized * topSpeed;

        //Debug.Log("playerRB.velocity " + playerRB.velocity);

    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject; 
        if (hit.name.Contains("Chair"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
        else if (hit.name.Contains("Chair"))
        {
            Destroy(gameObject);
            yield return new WaitForSeconds(2);
        }
    }
}
