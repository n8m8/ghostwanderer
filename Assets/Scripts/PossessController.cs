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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (Input.GetKeyDown("r") && other.tag == "Player")
        {
            if (isPossessing)
            {
                ParticleSystem system = Protag.gameObject.GetComponentInChildren<ParticleSystem>();
                system.Stop();
                ParticleSystem system1 = objectToPossess.gameObject.GetComponentInChildren<ParticleSystem>();
                system1.Stop();
                Protag.gameObject.GetComponent<Renderer>().enabled = true;
                isPossessing = false;
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
            }
            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        }

    }
}
