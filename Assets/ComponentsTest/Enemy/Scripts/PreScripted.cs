using System.Collections;
using Pathfinding;
using UnityEngine;
public class PreScripted : MonoBehaviour
{

    public Transform[] points;
    private AIDestinationSetter agent;



    // Use this for initialization
    void Start()
    {
        agent = GetComponent<AIDestinationSetter>();
        //come in
        agent.target = points[0];
        StartCoroutine(playAnimation());

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator playAnimation(){
        //Input animation here
        yield return new WaitForSeconds(3f);
        //leave
        agent.target = points[1];

        yield return new WaitForSeconds(2f);

        this.gameObject.SetActive(false);

    }





}
