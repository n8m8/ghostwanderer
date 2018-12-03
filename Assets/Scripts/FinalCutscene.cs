using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCutscene : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject soundController;
	[SerializeField] private GameObject blackScreen;
	[SerializeField] private GameObject finalBoss;
	[SerializeField] private AudioClip lightSwitchSFX;
	[SerializeField] private AudioClip footstepsSFX;
	[SerializeField] private AudioClip gunshotSFX;
	[SerializeField] private AudioClip ghostTransitionSFX;

	private AudioSource SFXPlayer;
	private bool finalDialogueOpened;
	private bool endingUnplayed;


	private TestPlayerMove ghostScript;

	// Use this for initialization
	void Start () {
		ghostScript = player.GetComponent<TestPlayerMove>();
		SFXPlayer = GetComponent<AudioSource> ();
		finalDialogueOpened = false;
		endingUnplayed = true;
	}

	void Update () {
		finalDialogueOpened = finalDialogueOpened || finalBoss.GetComponent<UnavoidableDialogue> ().inProgress;
		if (finalDialogueOpened && !(finalBoss.GetComponent<UnavoidableDialogue> ().inProgress) && endingUnplayed) {
			endingUnplayed = false;
			StartCoroutine (EndingAnimation ());
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"){
			//disable input
			ghostScript.DisableMovement();
			StartCoroutine(PlayAnimation());
		}
	}

	private IEnumerator PlayAnimation(){
		SFXPlayer.clip = lightSwitchSFX;
		SFXPlayer.Play ();
		soundController.SetActive (false);
		blackScreen.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.8f);
		SFXPlayer.clip = footstepsSFX;
		SFXPlayer.Play ();
		yield return new WaitForSeconds (1.7f);
		SFXPlayer.Stop ();
		SFXPlayer.clip = lightSwitchSFX;
		SFXPlayer.Play ();
		blackScreen.GetComponent<SpriteRenderer> ().enabled = false;
		finalBoss.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		finalBoss.GetComponent<UnavoidableDialogue> ().enabled = true;
	}

	private IEnumerator EndingAnimation(){
		SFXPlayer.clip = lightSwitchSFX;
		SFXPlayer.Play ();
		blackScreen.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (0.8f);
		SFXPlayer.clip = gunshotSFX;
		SFXPlayer.Play ();
		yield return new WaitForSeconds(4f);
		SFXPlayer.clip = ghostTransitionSFX;
		SFXPlayer.volume = 0.2f;
		SFXPlayer.Play ();
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene ("MainMenu");
	}
}