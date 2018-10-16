using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	[SerializeField] private GameObject humanMusic;
	[SerializeField] private GameObject spiritMusic;
	[SerializeField] private GameObject switchSoundFX;
	private AudioSource humanAudioSource;
	private AudioSource spiritAudioSource;
	private AudioSource switchSource;

	public bool isGhost = false;
	private bool wasGhost = false;
	private bool isCrossfading;

	[SerializeField] private float crossfadeTime;
	// Use this for initialization
	void Start () {
		humanAudioSource = humanMusic.GetComponent<AudioSource> ();
		spiritAudioSource = spiritMusic.GetComponent<AudioSource> ();
		switchSource = switchSoundFX.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGhost != wasGhost) {
			isCrossfading = true;
			switchSource.time = 0.3f;
			switchSource.Play ();
		}

		if (isCrossfading) {
			if (isGhost) {
				if (spiritAudioSource.volume >= 1.0f - Time.deltaTime) {
					spiritAudioSource.volume = 1.0f;
					humanAudioSource.volume = 0.0f;
					isCrossfading = false;
				} else {
					humanAudioSource.volume -= 1.0f * Time.deltaTime * crossfadeTime;
					spiritAudioSource.volume += 1.0f * Time.deltaTime * crossfadeTime;
				}
			} else {
				if (spiritAudioSource.volume <= 0.0f + Time.deltaTime) {
					spiritAudioSource.volume = 0.0f;
					humanAudioSource.volume = 1.0f;
					isCrossfading = false;
				} else {
					humanAudioSource.volume += 1.0f * Time.deltaTime * crossfadeTime;
					spiritAudioSource.volume -= 1.0f * Time.deltaTime * crossfadeTime;
				}
			}
		}

		wasGhost = isGhost;
	}
}
