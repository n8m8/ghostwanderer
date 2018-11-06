using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingObject : MonoBehaviour
{

    public bool canMoveObject = false;

    public GameObject enemy;

    private StationAI enemyController;
    private PatrolingAI enemyController2;

    // Use this for initialization
    void Start()
    {
        enemyController = enemy.GetComponent<StationAI>();
        enemyController2 = enemy.GetComponent<PatrolingAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMoveObject = true;
        }

        if (Input.GetKeyDown("f") && canMoveObject)
        {
            if (enemyController != null)
            {
                enemyController.distracted = true;
            }

            if(enemyController2 != null){
                enemyController2.distracted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canMoveObject = false;
        }

    }
}
