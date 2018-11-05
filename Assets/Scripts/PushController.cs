using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour {

    [SerializeField] private GameObject PushableObject;
    [SerializeField] private GameObject Protag;
    [SerializeField] private GameObject Enemy;

    public bool canMoveObject = false;
    public bool enemyInRange = false;
    public bool destroyed = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Enemy.transform.position, transform.position) < 4)
        {
            enemyInRange = true;
        }

        if (Input.GetKeyDown("f") && canMoveObject && enemyInRange && !destroyed)
        {

            PushableObject.GetComponent<Renderer>().enabled = false;
            //Destroy(levelController.Vase);
            //Destroy(levelController.VaseObject);
            Destroy(Enemy);
            AudioSource soundFX = PushableObject.GetComponent<AudioSource>();
            soundFX.Play();
            destroyed = true;
        }
        else if (Input.GetKeyDown("f") && canMoveObject && !destroyed)
        {
            destroyed = false;
            PushableObject.GetComponent<Renderer>().enabled = true;
        }
        enemyInRange = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canMoveObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canMoveObject = false;
        }

    }
}
