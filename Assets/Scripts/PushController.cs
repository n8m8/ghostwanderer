using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour {

    [SerializeField] private GameObject PushableObject;
    [SerializeField] private GameObject Protag;
    [SerializeField] private GameObject Enemy;

    public bool canMoveObject = false;
    public bool enemyInRange = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMoveObject = true;
        }

        if (Vector3.Distance(Enemy.transform.position, transform.position) < 4)
        {
            enemyInRange = true;
        }

        if (Input.GetKeyDown("f") && canMoveObject && enemyInRange)
        {

            PushableObject.GetComponent<Renderer>().enabled = false;
            //Destroy(levelController.Vase);
            //Destroy(levelController.VaseObject);
            Destroy(Enemy);
        }
        enemyInRange = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canMoveObject = false;
        }

    }
}
