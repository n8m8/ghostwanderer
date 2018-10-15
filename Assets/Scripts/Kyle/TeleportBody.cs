using UnityEngine;

public class TeleportBody : MonoBehaviour {

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform bodyDestination;

    private TestPlayerMove ghostScript;

    // Use this for initialization
    void Start () {
        ghostScript = player.GetComponent<TestPlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == player.tag && !ghostScript.isGhost)
        {
            body.transform.position = bodyDestination.position;
        }
    }
}
