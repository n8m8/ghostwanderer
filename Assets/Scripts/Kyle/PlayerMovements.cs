using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kyle
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float abs_force = 50.0f;
    [SerializeField] private float topSpeed = 6.0f;
    [SerializeField] private float friction = -10.0f;

    private Vector2 movement;
    private Rigidbody2D playerRB;
    private PlayerController.PlayerStatus playerStatus;
    private LayerController layerController;
    private TouchInfo touchInfo;

    private readonly float TAN27 = 1.96261050551f;

    // Use this for initialization
    void Start()
    {
        layerController = GetComponent<LayerController>();
        touchInfo = layerController.touchInfo;
        playerRB = GetComponent<Rigidbody2D>();
        playerStatus = this.GetComponent<PlayerController>().playerStatus;
        playerStatus.moveAllowed = true;
        ParticleSystem system = gameObject.GetComponentInChildren<ParticleSystem>();
        system.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;
        //float y = Input.GetAxis("Vertical") * Time.deltaTime * ySpeed;
        if (Input.GetKeyDown("q") && canGhost)
        {
            toggleGhostMode();
        }
        //transform.Translate(x, y, 0.0f);

    }

    bool canGhost = false;
     void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ghost Portal")
        {
                canGhost = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canGhost = false;
    }
    private void FixedUpdate()
    {
        if (playerStatus.moveAllowed)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Move(h, v);
            //UpdateLayers();
        }
    }

    private void Move(float h, float v)
    {
        //Modifies movement to match the angles of the tile map.
        h = h * TAN27;

        movement.Set(h, v);
        movement = movement.normalized * abs_force * Time.deltaTime;

        playerRB.AddForce(movement);

        if (playerRB.velocity.magnitude > topSpeed)
            playerRB.velocity = playerRB.velocity.normalized * topSpeed;

        playerRB.AddForce(playerRB.velocity * friction);
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
