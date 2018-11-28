using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingObject : MonoBehaviour
{

    public bool canMoveObject = false;
	[SerializeField] private GameObject obj;
    public GameObject enemy;
	[SerializeField] private float duration;
	[SerializeField] private bool staysOnForever;
	private float timeOn;
	private bool tvOn;

    private StationAI enemyController;
    private PatrolingAIDistrac enemyController2;

    // Use this for initialization
    void Start()
    {
        enemyController = enemy.GetComponent<StationAI>();
        enemyController2 = enemy.GetComponent<PatrolingAIDistrac>();
		timeOn = 0;
		tvOn = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown ("e") && canMoveObject && !tvOn)
			turnOn ();
		if (tvOn && !staysOnForever){
			timeOn += Time.deltaTime;
			if (timeOn > duration)
				turnOff ();
		}
    }

	void turnOn(){
		if (enemyController != null)
		{
			enemyController.distracted = true;
		}

		if (enemyController2 != null)
		{
			enemyController2.distracted = true;
		}
		if (obj.GetComponent<AudioSource>() != null)
			obj.GetComponent<AudioSource>().Play();
		try
		{
			GetComponentInParent<Animator>().SetTrigger("Lure");
		}
		catch {
			Debug.Log ("lure bug " + transform.name);
		}
		tvOn = true;
	}

	void turnOff(){
		if (enemyController != null)
		{
			enemyController.distracted = false;
		}

		if (enemyController2 != null)
		{
			enemyController2.distracted = false;
		}
		if (obj.GetComponent<AudioSource>() != null)
			obj.GetComponent<AudioSource>().Stop();
		try
		{
			GetComponentInParent<Animator>().SetTrigger("Stop");
		}
		catch {
			Debug.Log ("lure bug " + transform.name);
		}
		tvOn = false;
		timeOn = 0;
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
        if (collision.tag == "Player")
        {
            canMoveObject = false;
        }

    }
}
