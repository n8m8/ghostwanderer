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

    private readonly float TAN27 = 1.96261050551f;

    // Use this for initialization
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        playerStatus = this.GetComponent<PlayerController>().playerStatus;
=======
        ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
        system.Stop();
>>>>>>> level-one
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * ySpeed;

        //transform.Translate(x, y, 0.0f);
        if (Input.GetKeyDown("e"))
        {
            toggleGhostMode();
        }



    }

    private void FixedUpdate()
    {
        if (playerStatus.moveAllowed)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Move(h, v);
            playerRB.AddForce(playerRB.velocity * friction);
        }
        else
        {
            Debug.Log("move allowed not working");
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
            isGhost = false;
            PC.isTrigger = false;
            ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            system.Stop();

        }
        else
        {
            PC.isTrigger = true;
            isGhost = true;
            ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
            system.Play();
        }
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (isGhost)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }

                 yield return new WaitForSeconds(0);

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
