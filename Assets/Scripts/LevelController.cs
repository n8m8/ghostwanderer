using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    //True if the player is a ghost, false otherwise
    [HideInInspector] public bool playerIsGhost = false;

    //Arraylist of the rooms in the level
    [SerializeField] private ArrayList rooms;
    //ArrayList of the enemies in the level
    [SerializeField] private ArrayList enemies;
    //bool if the conditions are met to load the next level
    [SerializeField] private bool levelComplete = false;
    //The next level
    [SerializeField] private string nextLevel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (levelComplete)
        {
            LoadNextLevel();
        }
	}

    // Move to the next level (Public method)
    public void LoadNextLevel()
    {
        StartCoroutine(NextLevelAnimation());
    }

    // Animation for ending the level
    private IEnumerator NextLevelAnimation()
    {
        //TODO: Add lighting / zoom camera animation
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextLevel);
    }

    // Switch to the Spirit World
    public void SwitchToSpiritWorld()
    {
        StartCoroutine(SpiritWorldAnimation());
    }

    // Animation for entering the Spirit World
    private IEnumerator SpiritWorldAnimation()
    {
        // TODO: Add some lighting / zoom camera animation
        yield return new WaitForSeconds(1.5f);
        playerIsGhost = true;
        yield return new WaitForSeconds(0.5f);
    }

    // Switch to the Real World
    public void SwitchToRealWorld()
    {
        StartCoroutine(RealWorldAnimation());
    }

    // Animation for reentering the Real World
    private IEnumerator RealWorldAnimation()
    {
        // TODO: Add some lighting / zoom camera animation
        yield return new WaitForSeconds(1.5f);
        playerIsGhost = false;
        yield return new WaitForSeconds(0.5f);
    }

}
