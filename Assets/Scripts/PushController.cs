using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour {

    [SerializeField] private GameObject PushableObject;
    [SerializeField] private GameObject Protag;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Enemy2;
    [SerializeField] private Animator VaseAnimator;
	[SerializeField] private AudioClip soundFX;
	[SerializeField] private AudioClip reverse;

    public bool canMoveObject = false;
    public bool enemyInRange = false;
    public bool enemyInRange2 = false;
    public bool destroyed = false;
    private bool enemy1Dead = false;
    private bool enemy2Dead = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Enemy != null)
        {
            if (Vector3.Distance(Enemy.transform.position, transform.position) < 4)
            {
                enemyInRange = true;
            }
        }
        if (Enemy2 != null)
        {
            if (Vector3.Distance(Enemy2.transform.position, transform.position) < 4)
            {
                enemyInRange2 = true;
            }
        }

		if (Input.GetKeyDown("e") && canMoveObject && !destroyed && Protag.GetComponent<TestPlayerMove>().isGhost)
        {
			VaseAnimator.SetTrigger ("Fall");
			if (enemyInRange) {
				Destroy (Enemy);
				enemy1Dead = true;
			} 
			if(enemyInRange2){
				Destroy (Enemy2);
				enemy2Dead = true;
			}

			if (PushableObject.GetComponent<AudioSource> () != null && soundFX!=null) {
				AudioSource source = PushableObject.GetComponent<AudioSource> ();
				source.clip = soundFX;
				PushableObject.GetComponent<AudioSource> ().Play ();
			}
				
			destroyed = true;
        }
        else if (Input.GetKeyDown("e") && canMoveObject && destroyed)
        {
            destroyed = false;
            PushableObject.GetComponent<Renderer>().enabled = true;
            VaseAnimator.SetTrigger("Reverse");

			if (PushableObject.GetComponent<AudioSource> () != null && reverse!=null) {
				AudioSource source = PushableObject.GetComponent<AudioSource> ();
				source.clip = reverse;
				PushableObject.GetComponent<AudioSource> ().Play ();
			}
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
