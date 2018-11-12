using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessController : MonoBehaviour
{


    [SerializeField]
    private GameObject Protag;
    [SerializeField]
    private GameObject objectToPossess;

    public bool isPossessing = false;
    private bool canPossess = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r") && canPossess && Protag.GetComponent<TestPlayerMove>().isGhost){
            if (isPossessing)
            {
                ParticleSystem system = Protag.gameObject.GetComponentInChildren<ParticleSystem>();
                system.Stop();
                ParticleSystem system1 = objectToPossess.gameObject.GetComponentInChildren<ParticleSystem>();
                system1.Stop();
                Protag.gameObject.GetComponent<Renderer>().enabled = true;
                isPossessing = false;
                Protag.GetComponent<TestPlayerMove>().abs_force = 1000;
            }
            else
            {
                ParticleSystem system = Protag.gameObject.GetComponentInChildren<ParticleSystem>();
                system.Play();
                ParticleSystem system1 = objectToPossess.gameObject.GetComponentInChildren<ParticleSystem>();
                system1.Play();
                Protag.gameObject.GetComponent<Renderer>().enabled = false;
                //Destroy(levelController.Vase);
                //Destroy(levelController.VaseObject);
                isPossessing = true;
                Protag.GetComponent<TestPlayerMove>().abs_force = 0;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            canPossess = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canPossess = false;
        }

    }
}
