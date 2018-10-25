using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour {
    [SerializeField] private string previousScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BackToScene()
    {
        SceneManager.LoadScene(previousScene);
    }
}
