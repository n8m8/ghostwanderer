using UnityEngine;

public class FindBody : MonoBehaviour
{

    [SerializeField] private GameObject player;

    private TestPlayerMove ghostScript;

    // Use this for initialization
    void Start()
    {
        ghostScript = player.GetComponent<TestPlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == player.tag && ghostScript.isGhost && !ghostScript.GetBodyAvailable())
        {
            ghostScript.FindBody();
        }
    }
}
