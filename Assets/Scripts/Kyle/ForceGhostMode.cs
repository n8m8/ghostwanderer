using UnityEngine;

public class ForceGhostMode : MonoBehaviour {

    [SerializeField] private GameObject player;

    private TestPlayerMove ghostScript;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == player.tag && !ghostScript.isGhost)
        {
            ghostScript.EnableGhostMode();
            Destroy(GameObject.Find("TestPlayer(Clone)"));
            //Camera.main.transform.position = destination.transform.position + new Vector3(0, 0, -20);
        }
    }
}
