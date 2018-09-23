using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChasingAI : MonoBehaviour {
	private GameObject playerObject;

	NavMeshAgent agent;
	Animator anim;

	void Start() {
		agent = GetComponent<NavMeshAgent> ();
        playerObject = GameObject.Find("Player");
    }
	
	void Update () {
        agent.SetDestination(playerObject.transform.position);
        //if very far away zombie speed = 0: idle animation
        //if somewhat close zombie speed = walking speed: walking animation
        //if near zombie speed = 8.0*walking speed:running animation
        //transform.Translate(0, 0, _multiplier*speed * Time.deltaTime);
    }

	void OnTriggerEnter(Collider other){

		if (other.gameObject == playerObject) {
            other.enabled = false;
            this.enabled = false;
		}
	}



	//private void OnSpeedChanged(float value) {
	//	speed = baseSpeed * value;
	//}
}
