using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShootingAI : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform[] points;
    public float bulletspeed;
    private int desPoint = 0;
    private NavMeshAgent agent;
    private bool isPlayerdiscovered;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
            
        agent.destination = points[desPoint].position;
        desPoint = (desPoint + 1) % points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerdiscovered)
        {

            //here i need to run a special script which will start the chase mechanisim
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 8.0f)
                GotoNextPoint();
        }
    }

    //shoot function, simple
    void Shoot()
    {
        //audio.PlayOneShot(shotS);
        Rigidbody something = Instantiate(bullet, transform.position, transform.rotation);
        something.velocity = transform.TransformDirection(new Vector3(0, 0, bulletspeed));
        Destroy(something.gameObject, 0.5f);
    }
}
