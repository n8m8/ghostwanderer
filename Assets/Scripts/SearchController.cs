using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchController : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    private bool itemZone = false;
    public static bool inProgress = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && itemZone && !inProgress)
        {
            StartCoroutine(briefMessage("Found something!", 2));
        }
        else if (Input.GetKeyDown(KeyCode.R) && !itemZone && !inProgress)
        {
            StartCoroutine(briefMessage("Nothing", 1));
        }
    }

    IEnumerator briefMessage(string message, int time)
    {
        if (!inProgress)
        {
            textDisplay.text = message;
            yield return new WaitForSeconds(time);
            textDisplay.text = "";
        }
    }

    //checkflag is true if player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ItemZone")
        {
            itemZone = true;
        }
    }

    //Reset the checkflag 
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "ItemZone")
        {
            itemZone = false;
        }

    }
}
