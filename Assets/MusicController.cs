using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	[SerializeField] private GameObject humanMusic;
	[SerializeField] private GameObject spiritMusic;
	private AudioSource humanAudioSource;
	private AudioSource spiritAudioSource;

	public bool isGhost;
	private bool wasGhost;
	private bool isCrossfading;

	[SerializeField] private float crossfadeTime;
	// Use this for initialization
	void Start () {
		humanAudioSource = humanMusic.GetComponent<AudioSource> ();
		spiritAudioSource = spiritMusic.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isGhost != wasGhost)
			isCrossfading = true;
		
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
