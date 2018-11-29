using System.Collections;
using UnityEngine;

public class ForceGhostMode : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject scientist;
	[SerializeField] private GameObject soundController;
	[SerializeField] private GameObject blackScreen;
	[SerializeField] private GameObject evidence;

    private TestPlayerMove ghostScript;
    private float originalSpeed;
	private bool evidenceClosed;
	public bool cutsceneStarted;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
		evidenceClosed = false;
		cutsceneStarted = false;
    }

	void Update() {
		if (evidence == null && Input.GetKeyDown ("e"))
			evidenceClosed = true;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.tag == player.tag && !ghostScript.isGhost && evidenceClosed && !cutsceneStarted)
        {
			cutsceneStarted = true;
            scientist.SetActive(true);
            GameObject.Find("Door_Exit").GetComponentInChildren<DoorTrigger>().isLocked = true;
            GameObject.Find("Door_Exit_Real").GetComponentInChildren<DoorTrigger>().isLocked = false;

            //disable input
            ghostScript.DisableMovement();
            StartCoroutine(PlayAnimation());
        }
    }

    private IEnumerator PlayAnimation(){
		yield return new WaitForSeconds(1f);
		GetComponent<AudioSource> ().Play ();
        yield return new WaitForSeconds(1.45f);
		soundController.SetActive (false);
		blackScreen.SetActive (true);
		yield return new WaitForSeconds(4f );
        player.GetComponent<Animator>().SetBool("isGhost", true);
        yield return new WaitForSeconds(0f);
        ghostScript.EnableMovement();
		//sets player's location to chest
		ghostScript.EnableGhostMode(new Vector3(3.208198f,-3.886554f,0f)); 
        Destroy(GameObject.Find("TestPlayer(Clone)"));
        this.GetComponent<EdgeCollider2D>().enabled = false;
        ghostScript.ChangeRadius(7f);
		StartCoroutine (GetComponent<Spawn> ().delaySpawn ());
		blackScreen.SetActive (false);
		soundController.SetActive (true);
        //Camera.main.transform.position = destination.transform.position + new Vector3(0, 0, -20);
    }


}
