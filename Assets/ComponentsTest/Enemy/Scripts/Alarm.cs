using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour
{

    private GameObject player;
    private TestPlayerMove playerControl;
    public bool isOn;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<TestPlayerMove>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && playerControl.isGhost == false){
            isOn = true;
        }
    }
}
