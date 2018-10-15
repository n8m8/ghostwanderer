using UnityEngine;

public class Teleport : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject destination;
    [SerializeField] private CameraController cameraController;

    private TestPlayerMove ghostScript;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == player.tag && !ghostScript.isGhost)
        {
            player.transform.position = destination.transform.position;
            cameraController.SwitchFocusArea(destination.transform.position);
            //Camera.main.transform.position = destination.transform.position + new Vector3(0, 0, -20);
        }
    }

}
