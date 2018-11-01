using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

// Kyle
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
    //The camera to reset position
    [SerializeField] private CameraController cameraController;
    // The player controller
    [SerializeField] private GameObject player;

    [SerializeField] public GameObject Vase;

    public GameObject VaseObject;
    //The current checkpoint
    public GameObject currentCheckpoint;

    // Postprocessing variables
    private Grain grainPost;
    private ChromaticAberration chromePost;
    private ColorGrading gradingPost;

    private PlayerController.PlayerStatus playerStatus;

    // Use this for initialization
    void Start () {
        VaseObject = Vase;
        playerStatus = player.GetComponent<PlayerController>().playerStatus;
        
        // Get post processing info
        var cameraPostProcessing = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>();
        cameraPostProcessing.profile.TryGetSettings<Grain>(out grainPost);
        cameraPostProcessing.profile.TryGetSettings<ChromaticAberration>(out chromePost);
        cameraPostProcessing.profile.TryGetSettings<ColorGrading>(out gradingPost);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

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
        enablePostProcessing();
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
        disablePostProcessing();
        yield return new WaitForSeconds(0.5f);
    }

    // Respawns Player at current Checkpoint
    public void RespawnPlayer(){
        StartCoroutine(RespawnAnimation());
	}

    // Animation for respawning the player
    private IEnumerator RespawnAnimation()
    {
        player.GetComponent<PlayerController>().playerStatus.moveAllowed = false;
        GameObject.Find("Test Player").transform.position = currentCheckpoint.transform.position;
        yield return new WaitForSeconds(3f);
        player.GetComponent<PlayerController>().playerStatus.moveAllowed = true;
    }


    private void enablePostProcessing()
    {
        grainPost.enabled.value = false;
        chromePost.enabled.value = false;
        gradingPost.enabled.value = false;
    }

    private void disablePostProcessing()
    {
        grainPost.enabled.value = true;
        chromePost.enabled.value = true;
        gradingPost.enabled.value = true;
    }

}
