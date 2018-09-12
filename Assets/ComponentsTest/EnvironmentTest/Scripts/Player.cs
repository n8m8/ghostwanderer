using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Just for test pruposes, will not cosider any structure purposes;
public class Player : MonoBehaviour {

    const string indi_text1 = "No Collider";

    public float force = 60f;
    public float topSpeed = 6.0f;
    public Text IndicateText1;

    private LevelController LC_script;
    private GameObject otherGO;

    private Vector3 movement;
    private Rigidbody playerRB;
    private bool can_use_trigger = false;

    // Use this for initialization
    void Start () {
        playerRB = GetComponent<Rigidbody>();
        LC_script = GameObject.Find("LevelController").GetComponent<LevelController>();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(canbetriggered + " " + OtherGO);

        if (can_use_trigger)
        {
            if(otherGO.GetComponent<InteractableOBJ>().trigger1enable)
            {
                IndicateText1.text = "Press E";
                if (Input.GetKeyDown(KeyCode.E))
                {
                    otherGO.GetComponent<InteractableOBJ>().trigger1();
                }
            }

            if (otherGO.GetComponent<InteractableOBJ>().trigger2enable)
            {
                IndicateText1.text = "Press Q";
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    otherGO.GetComponent<InteractableOBJ>().trigger2();
                }
            }
        }
        else
        {
            IndicateText1.text = indi_text1;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            LC_script.toggle_phase();
            
        }

    }

    //private void LateUpdate()
    //{
    //    canbetriggered = false;
    //}

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
    }

    private void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * force * Time.deltaTime;

        //Debug.Log("movement " + movement);

        //playerRB.MovePosition(transform.position + movement);
        playerRB.AddForce(movement * force);


        if (playerRB.velocity.magnitude > topSpeed)
            playerRB.velocity = playerRB.velocity.normalized * topSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractableOBJ")
        {
            otherGO = other.transform.parent.gameObject;
            can_use_trigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        can_use_trigger = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    canbetriggered = true;

    //}

}
